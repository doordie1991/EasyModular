using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Auth
{
    /// <summary>
    /// 权限配置模型
    /// </summary>
    public class PermissionConfigModel
    {
        /// <summary>
        /// 是否开启权限认证
        /// </summary>
        public bool IsValidate { get; set; }

        /// <summary>
        /// 是否启用单账户登录
        /// </summary>
        public bool IsSingleAccount { get; set; }
    }
}
