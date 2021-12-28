using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Infrastructure
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public static class RoleKey
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员角色编码")]
        public const string ROLE_SUPER_CODE = "ROLE_SUPER";

        /// <summary>
        /// 运营商角色编码
        /// </summary>
        [Description("运营商角色编码")]
        public const string ROLE_OPERATOR_CODE = "ROLE_OPERATOR";

        /// <summary>
        /// 企业管理员角色编码
        /// </summary>
        [Description("企业管理员角色编码")]
        public const string ROLE_ENTERPRISE_CODE = "ROLE_ENTERPRISE";

    }
}
