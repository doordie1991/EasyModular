using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 权限数据来源
    /// </summary>
    public enum PermissionSource
    {
        [Description("手动")]
        Manual = 0,
        [Description("自动")]
        Auto = 1
    }
}
