using Demo.Admin.Application;
using Demo.Admin.Application.Auth;
using Demo.Admin.Application.LoginLogService;
using EasyModular;
using EasyModular.Auth;
using EasyModular.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Web.Controllers
{
    [Description("身份认证")]
    public class AuthController : ModuleController
    {
        private readonly IAuthService _authService;
        private readonly ILoginLogService _loginLogService;
        private readonly ILoginInfo _loginInfo;
        private readonly IJwtHandler _jwtHandler;
        private readonly WebConfigModel _webConfigModel;
        private IHttpContextAccessor _accessor;

        public AuthController(IAuthService authService, ILoginLogService loginLogService, ILoginInfo loginInfo, IJwtHandler jwtHandler, WebConfigModel webConfigModel, IHttpContextAccessor accessor)
        {
            _authService = authService;
            _loginLogService = loginLogService;
            _loginInfo = loginInfo;
            _jwtHandler = jwtHandler;
            _webConfigModel = webConfigModel;
            _accessor = accessor;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditingAttribute]
        [Description("获取验证码")]
        public IResultModel VerifyCode(int length = 4)
        {
            return _authService.CreateVerifyCode(length);
        }

        [HttpPost]
        [AllowAnonymous]
        [Description("登录")]
        public async Task<IResultModel> Login([FromBody] LoginModel model)
        {
            var log = new LoginLogAddModel();
            try
            {
                var result = await _authService.Login(model, _webConfigModel.RefreshTokenExpiredTime);

                log.Result = result.Successful;
                log.IP = _loginInfo.IP;
                log.BrowserInfo = _accessor.HttpContext.Request.Headers["User-Agent"];
                log.UserCode = model.UserCode;
                log.Remark = result.Msg;

                if (result.Successful)
                {
                    var user = result.Data.User;
                    var loginInfo = result.Data.AuthInfo;

                    log.UserName = user.UserName;

                    var claims = new[]
                    {
                       new Claim(ClaimsName.UserId, user.Id.ToString()),
                       new Claim(ClaimsName.UserCode,user.UserCode),
                       new Claim(ClaimsName.UserName, user.UserName),
                       new Claim(ClaimsName.OrganizeId, user.OrganizeId.ToString()),
                       new Claim(ClaimsName.LoginTime, loginInfo.LoginTime.ToString()),
                       new Claim(ClaimsName.RoleIds, result.Data.RoleIds),
                       new Claim(ClaimsName.RoleCodes, result.Data.RoleCodes),
                       new Claim(ClaimsName.RoleNames, result.Data.RoleNames)
                    };


                    var token = _jwtHandler.Build(claims, loginInfo.RefreshToken);
                    return ResultModel.Success(token);
                }

                return ResultModel.Failed(result.Msg);
            }
            catch (Exception ex)
            {
                log.Remark = ex.Message;
                return ResultModel.Failed(ex.Message);
            }
            finally
            {
                await _loginLogService.Add(log);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Description("刷新令牌")]
        public async Task<IResultModel> RefreshToken([BindRequired] string refreshToken)
        {
            var result = await _authService.RefreshToken(refreshToken);
            if (result.Successful)
            {
                var user = result.Data.User;
                var loginInfo = result.Data.AuthInfo;

                var claims = new[]
                {
                     new Claim(ClaimsName.UserId, user.Id.ToString()),
                     new Claim(ClaimsName.UserCode,user.UserCode),
                     new Claim(ClaimsName.UserName, user.UserName),
                     new Claim(ClaimsName.OrganizeId, user.OrganizeId.ToString()),
                     new Claim(ClaimsName.LoginTime, loginInfo.LoginTime.ToString()),
                     new Claim(ClaimsName.RoleIds, result.Data.RoleIds),
                     new Claim(ClaimsName.RoleCodes, result.Data.RoleCodes),
                     new Claim(ClaimsName.RoleNames, result.Data.RoleNames)
                };

                var token = _jwtHandler.Build(claims, refreshToken);
                return ResultModel.Success(token);
            }

            return ResultModel.Failed(result.Msg);
        }

        [HttpGet]
        [Common]
        [Description("获取认证信息")]
        public Task<IResultModel> AuthInfo()
        {
            return _authService.GetAuthInfo();
        }
    }
}
