using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EasyModular.Auth
{
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId
        {
            get
            {
                var userId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.UserId);

                if (userId == null || string.IsNullOrEmpty(userId.Value))
                {
                    return "";
                }

                return userId.Value;
            }
        }

        public string UserCode
        {
            get
            {
                var userCode = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.UserCode);

                if (userCode == null || string.IsNullOrEmpty(userCode.Value))
                {
                    return "";
                }

                return userCode.Value;
            }
        }

        public string UserName
        {
            get
            {
                var userName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.UserName);

                if (userName == null || string.IsNullOrEmpty(userName.Value))
                {
                    return "";
                }

                return userName.Value;
            }
        }


        public string OrganizeId
        {
            get
            {
                var organizeId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.OrganizeId);

                if (organizeId == null || string.IsNullOrEmpty(organizeId.Value))
                {
                    return "";
                }

                return organizeId.Value;
            }
        }

        public string RoleIds
        {
            get
            {
                var roleIds = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.RoleIds);

                if (roleIds == null || string.IsNullOrEmpty(roleIds.Value))
                {
                    return "";
                }

                return roleIds.Value;
            }
        }

        public string RoleCodes
        {
            get
            {
                var roleCodes = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.RoleCodes);

                if (roleCodes == null || string.IsNullOrEmpty(roleCodes.Value))
                {
                    return "";
                }

                return roleCodes.Value;
            }
        }

        public string RoleNames
        {
            get
            {
                var roleNames = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.RoleNames);

                if (roleNames == null || string.IsNullOrEmpty(roleNames.Value))
                {
                    return "";
                }

                return roleNames.Value;
            }
        }

        public string TenantId
        {
            get
            {
                var TenantId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.TenantId);

                if (TenantId == null || string.IsNullOrEmpty(TenantId.Value))
                {
                    return "";
                }

                return TenantId.Value;
            }
        }

        public string TenantName
        {
            get
            {
                var tenantName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.TenantName);

                if (tenantName == null || string.IsNullOrEmpty(tenantName.Value))
                {
                    return "";
                }

                return tenantName.Value;
            }
        }


        public string TenantType
        {
            get
            {
                var tenantType = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.TenantType);

                if (tenantType == null || string.IsNullOrEmpty(tenantType.Value))
                {
                    return "";
                }

                return tenantType.Value;
            }
        }

        public string Trade
        {
            get
            {
                var trade = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.Trade);

                if (trade == null || string.IsNullOrEmpty(trade.Value))
                {
                    return "";
                }

                return trade.Value;
            }
        }

        public string IP
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public string IPv4
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        public string IPv6
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
            }
        }

        public long LoginTime
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.LoginTime);

                if (ty == null)
                {
                    return Convert.ToInt32(ty.Value);
                }

                return 0L;
            }
        }
    }
}
