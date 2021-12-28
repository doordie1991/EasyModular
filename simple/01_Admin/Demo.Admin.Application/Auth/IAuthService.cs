using Demo.Admin.Domain;
using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.Auth
{
    /// <summary>
    /// 身份认证服务
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        IResultModel CreateVerifyCode(int length = 6);

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="model">登录模型</param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> Login(LoginModel model, int refreshTokenExpiredTime);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> RefreshToken(string refreshToken);

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetAuthInfo();
    }
}
