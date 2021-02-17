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

        public Guid UserId
        {
            get
            {
                var userId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.UserId);

                if (userId != null && string.IsNullOrEmpty(userId.Value))
                {
                    return new Guid(userId.Value);
                }

                return Guid.Empty;
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

        public string OrganizeCode
        {
            get
            {
                var organizeCode = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.OrganizeCode);

                if (organizeCode == null || string.IsNullOrEmpty(organizeCode.Value))
                {
                    return "";
                }

                return organizeCode.Value;
            }
        }

        public string OrganizeName
        {
            get
            {
                var organizeName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.OrganizeName);

                if (organizeName == null || string.IsNullOrEmpty(organizeName.Value))
                {
                    return "";
                }

                return organizeName.Value;
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
