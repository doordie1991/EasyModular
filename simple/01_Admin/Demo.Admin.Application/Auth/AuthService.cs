using AutoMapper;
using Demo.Admin.Application.UserService;
using Demo.Admin.Domain;
using Demo.Admin.Infrastructure;
using EasyModular;
using EasyModular.Auth;
using EasyModular.Cache;
using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ICacheHandler _cacheHandler;
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly IUserRepository _userRepository;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISkinRepository _skinRepository;
        private readonly IUserService _userService;
        private readonly IPasswordHandler _passwordHandler;

        public AuthService(
             IMapper mapper
            , ICacheHandler cacheHandler
            , ILoginInfo loginInfo
            , IUserRepository userRepository
            , IRoleUserRepository roleUserRepository
            , ITenantRepository tenantRepository
            , IUserAuthRepository userAuthRepository
            , IMenuRepository menuRepository
            , IPermissionRepository permissionRepository
            , ISkinRepository skinRepository
            , IUserService userService
            , IPasswordHandler passwordHandler
            )
        {
            _mapper = mapper;
            _cacheHandler = cacheHandler;
            _loginInfo = loginInfo;

            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
            _tenantRepository = tenantRepository;
            _userAuthRepository = userAuthRepository;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
            _skinRepository = skinRepository;
            _userService = userService;
            _passwordHandler = passwordHandler;
        }

        #region ==创建验证码==

        public IResultModel CreateVerifyCode(int length = 6)
        {
            var verifyCodeModel = new VerifyCodeResultModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Base64String = DrawingHelper.DrawVerifyCodeBase64String(out string code, length)
            };

            var key = string.Format(CacheKeys.VerifyCodeKey, verifyCodeModel.Id);

            //把验证码放到内存缓存中，有效期10分钟
            _cacheHandler.SetAsync(key, code, 10);

            return ResultModel.Success(verifyCodeModel);
        }

        #endregion

        #region ==登录认证==

        public async Task<ResultModel<LoginResultModel>> Login(LoginModel model, int refreshTokenExpiredTime)
        {
            var result = new ResultModel<LoginResultModel>();

            //检测验证码
            if (model.VerifyCode != null && !await CheckVerifyCode(result, model.VerifyCode))
                return result;

            //检测用户
            var user = await _userRepository.FirstAsync(m => m.UserCode == model.UserCode && m.IsDel == false);
            var checkUserResult = CheckUser(user);
            if (!checkUserResult.Successful)
                return result.Failed(checkUserResult.Msg);

            //检测密码
            if (!CheckPassword(result, model, user))
                return result;

            //更新登录信息
            var loginInfo = await UpdateLoginInfo(user, refreshTokenExpiredTime);
            if (loginInfo == null)
                return result.Failed("更新登录信息失败");

            //获取租户信息
            var tenant = await _tenantRepository.FirstAsync(m => m.Id == user.TenantId && m.IsDel == false);
            if (tenant == null)
                return result.Failed("租户不存在");

            //获取用户角色
            var roles = await _roleUserRepository.QueryByUserId(user.Id);

            //删除验证码缓存
            if (model.VerifyCode != null)
                await _cacheHandler.RemoveAsync(string.Format(CacheKeys.VerifyCodeKey, model.VerifyCode.Id));

            //清除账户的认证信息缓存
            await _cacheHandler.RemoveAsync(string.Format(CacheKeys.UserAuthInfo, user.Id));

            return result.Success(new LoginResultModel
            {
                User = user,
                AuthInfo = loginInfo,
                Tenant = tenant,
                RoleIds = string.Join(",", roles.Select(m => m.Id).ToList()),
                RoleCodes = string.Join(",", roles.Select(m => m.RoleCode).ToList()),
                RoleNames = string.Join(",", roles.Select(m => m.RoleName).ToList())
            });
        }

        /// <summary>
        /// 检测验证码
        /// </summary>
        private async Task<bool> CheckVerifyCode(ResultModel<LoginResultModel> result, VerifyCodeModel model)
        {
            if (model == null || model.Code.IsNull())
            {
                result.Failed("请输入验证码");
                return false;
            }

            if (model.Id.IsNull())
            {
                result.Failed("验证码不存在");
                return false;
            }

            var cacheCode = await _cacheHandler.GetAsync(string.Format(CacheKeys.VerifyCodeKey, model.Id));
            if (cacheCode.IsNull())
            {
                result.Failed("验证码不存在");
                return false;
            }

            if (!cacheCode.Equals(model.Code))
            {
                result.Failed("验证码有误");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检测用户
        /// </summary>
        private IResultModel CheckUser(UserEntity user)
        {
            if (user == null)
                return ResultModel.Failed("用户不存在");

            if (user.Status == UserStatus.Disabled)
                return ResultModel.Failed("该用户已禁用，请联系管理员");

            return ResultModel.Success();
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        private bool CheckPassword(ResultModel<LoginResultModel> result, LoginModel model, UserEntity user)
        {
            var password = _passwordHandler.Encrypt(user.UserCode, model.Password);
            if (!user.Password.Equals(password))
            {
                result.Failed("密码错误");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 更新登录信息
        /// </summary>
        private async Task<UserAuthEntity> UpdateLoginInfo(UserEntity user, int refreshTokenExpiredTime)
        {
            Task<bool> task;
            var entity = await _userAuthRepository.FirstAsync(m => m.UserId == user.Id);
            if (entity != null)
            {
                entity.LoginTime = DateTime.Now.ToTimestamp();
                entity.LoginIP = _loginInfo.IPv4;
                entity.RefreshToken = Guid.NewGuid().ToString();
                entity.ExpiredTime = DateTime.Now.AddDays(refreshTokenExpiredTime);

                task = _userAuthRepository.UpdateAsync(entity);
            }
            else
            {
                entity = new UserAuthEntity
                {
                    UserId = user.Id,
                    LoginTime = DateTime.Now.ToTimestamp(),
                    LoginIP = _loginInfo.IPv4,
                    RefreshToken = Guid.NewGuid().ToString(),
                    ExpiredTime = DateTime.Now.AddDays(refreshTokenExpiredTime)
                };
                task = _userAuthRepository.InsertAsync(entity);
            }
            return await task ? entity : null;
        }
        #endregion

        #region ==刷新令牌==

        public async Task<ResultModel<LoginResultModel>> RefreshToken(string refreshToken)
        {
            var result = new ResultModel<LoginResultModel>();
            var cacheKey = string.Format(CacheKeys.RefreshToken, refreshToken);
            if (!_cacheHandler.TryGetValue(cacheKey, out UserAuthEntity authInfo))
            {
                authInfo = await _userAuthRepository.FirstAsync(m => m.RefreshToken == refreshToken);
                if (authInfo == null)
                    return result.Failed("身份认证信息无效，请重新登录~");

                //加入缓存
                var expires = (int)(authInfo.ExpiredTime - DateTime.Now).TotalMinutes;
                await _cacheHandler.SetAsync(cacheKey, authInfo, expires);
            }

            if (authInfo.ExpiredTime <= DateTime.Now)
                return result.Failed("身份认证信息过期，请重新登录~");

            var user = await _userRepository.FirstAsync(authInfo.UserId);
            var checkAccountResult = CheckUser(user);
            if (!checkAccountResult.Successful)
                return result.Failed(checkAccountResult.Msg);

            //获取租户信息
            var tenant = await _tenantRepository.QueryById(user.TenantId);
            //获取用户角色
            var roles = await _roleUserRepository.QueryByUserId(user.Id);

            return result.Success(new LoginResultModel
            {
                User = user,
                AuthInfo = authInfo,
                Tenant = tenant,
                RoleCodes = string.Join(",", roles.Select(m => m.RoleCode).ToList()),
                RoleNames = string.Join(",", roles.Select(m => m.RoleName).ToList())
            });
        }

        #endregion

        #region ==获取认证信息==

        public async Task<IResultModel> GetAuthInfo()
        {
            var user = await _userRepository.FirstAsync(m => m.Id == _loginInfo.UserId && m.IsDel == false);

            if (user == null)
                return ResultModel.Failed("用户不存在");

            if (user.Status == UserStatus.Disabled)
                return ResultModel.Failed("用户已被禁用");

            var tenant = await _tenantRepository.FirstAsync(m => m.Id == user.TenantId && m.IsDel == false);
            if (tenant == null)
                return ResultModel.Failed("企业不存在");

            if (tenant.Status == TenantStatus.Disabled)
                return ResultModel.Failed("企业已被禁用");

            var roles = await _roleUserRepository.QueryByUserId(_loginInfo.UserId);
            var skin = await _skinRepository.FirstAsync(m => m.UserId == user.Id && m.IsDel == false);

            var model = new AuthInfoModel
            {
                UserId = user.Id,
                UserCode = user.UserCode,
                UserName = user.UserName,
                Sex = user.Sex,
                JobName = user.JobName,
                Phone = user.Phone,
                Email = user.Email,
                TenantId = tenant.Id,
                TenantName = tenant.TenantName,
                TenantType=tenant.TenantType,
                RoleIds = string.Join(",", roles.Select(m => m.Id).ToList()),
                RoleCodes = string.Join(",", roles.Select(m => m.RoleCode).ToList()),
                RoleNames = string.Join(",", roles.Select(m => m.RoleName).ToList()),
                Skin = skin
            };

            var getMenuTree = GetUserMenuTree(_loginInfo.UserId);

            model.Menus = await getMenuTree;

            //超级管理查询所有
            if (_loginInfo.RoleCodes.Contains(RoleKey.ROLE_SUPER_CODE))
            {
                model.Permissions = await _permissionRepository.GetListAsync(m => m.IsDel == false);
            }
            else
            {
                model.Permissions = await _permissionRepository.QueryByUserId(_loginInfo.UserId);
            }

            return ResultModel.Success(model);
        }

        #region ==获取账户的菜单树==
        /// <summary>
        /// 获取账户的菜单树
        /// </summary>
        /// <returns></returns>
        private async Task<List<UserMenuItem>> GetUserMenuTree(string userId)
        {
            var all = new List<UserMenuItem>();

            //超级管理查询所有
            if (_loginInfo.RoleCodes.Contains(RoleKey.ROLE_SUPER_CODE))
            {
                all = _mapper.Map<List<UserMenuItem>>(await _menuRepository.GetListAsync(m => m.IsDel == false));
            }
            else
            {
                //受控菜单+非受控菜单
                var controlledMenu = await _menuRepository.QueryByUserId(userId);
                var unControlledMenu = await _menuRepository.GetListAsync(m => m.IsControl == false && m.IsDel == false);

                all = _mapper.Map<List<UserMenuItem>>(controlledMenu.Union(unControlledMenu).Distinct(new MenuComparer()).ToList());
            }

            var tree = all.Where(e => e.ParentId == Guid.Empty.ToString("N")).OrderBy(e => e.Sort).ToList();

            tree.ForEach(menu =>
            {
                SetChildren(menu, all);
            });

            tree.ForEach(menu =>
            {
                SetRootId(menu, all, menu.Id);
            });

            return tree;
        }

        private void SetChildren(UserMenuItem parent, List<UserMenuItem> all)
        {
            parent.Children = all.Where(e => e.ParentId == parent.Id).OrderBy(e => e.Sort).ToList();

            foreach (var item in parent.Children)
            {
                if (item.Type == MenuType.Node)
                    SetChildren(item, all);
            }
        }

        private void SetRootId(UserMenuItem parent, List<UserMenuItem> all, string rootId)
        {
            foreach (var item in parent.Children)
            {
                item.RootId = rootId;

                if (item.Children != null && item.Children.Any())
                    SetRootId(item, item.Children, rootId);
            }
        }

        #endregion

        #endregion

    }
}
