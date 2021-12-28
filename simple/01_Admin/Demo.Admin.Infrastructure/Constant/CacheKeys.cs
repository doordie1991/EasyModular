using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Infrastructure
{
    public static class CacheKeys
    {
        /// <summary>
        /// 验证码 ADMIN:VERIFY:CODE:
        /// </summary>
        [Description("验证码")] public const string VerifyCodeKey = "ADMIN:VERIFY:CODE:{0}";

        /// <summary>
        /// 刷新令牌，缓存用户认证信息 ADMIN:AUTH:REFRESHTOKEN:刷新令牌
        /// </summary>
        [Description("刷新令牌")] public const string RefreshToken = "ADMIN:AUTH:REFRESHTOKEN:{0}";

        /// <summary>
        /// 用户信息缓存 ADMIN:USER:用户编号
        /// </summary>
        [Description("用户信息缓存")] public const string User = "ADMIN:USER:{0}:INFO";

        /// <summary>
        /// 用户认证信息 ADMIN:USER:用户编号:
        /// </summary>
        [Description("用户认证信息")] public const string UserAuthInfo = "ADMIN:USER:{0}";

        /// <summary>
        /// 用户权限列表 ADMIN:USER:用户编号:PERMISSIONS:
        /// </summary>
        [Description("用户权限列表")] public const string UserPermissions = "ADMIN:USER:{0}:PERMISSIONS:{1}";

        /// <summary>
        /// 权限树 ADMIN:PERMISSION:TREE
        /// </summary>
        [Description("权限树缓存")] public const string PermissionTree = "ADMIN:PERMISSION:TREE";

        /// <summary>
        /// 系统用户列表
        /// </summary>
        [Description("系统用户列表")]
        public const string USER_LIST = "ADMIN:USER_LIST";

        /// <summary>
        /// 系统组织架构列表
        /// </summary>
        [Description("系统组织架构列表")]
        public const string ORGANIZATIONT_LIST = "ADMIN:ORGANIZATIONT_LIST";

        /// <summary>
        /// 系统组织架构树
        /// </summary>
        [Description("系统组织架构树")]
        public const string ORGANIZATIONT_TREE = "ADMIN:ORGANIZATIONT_TREE";

        /// <summary>
        /// 菜单树
        /// </summary>
        [Description("菜单树")]
        public const string MENU_TREE = "ADMIN:MENU_TREE";
    }
}
