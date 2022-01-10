using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    public class LoginResultModel
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserEntity User { get; set; }

        /// <summary>
        /// 认证信息
        /// </summary>
        public UserAuthEntity AuthInfo { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCodes { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleNames { get; set; }

    }
}
