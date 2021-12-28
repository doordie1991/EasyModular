using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 状态（0:创建、1:启用、-1:禁用）
    /// </summary>
    public enum UserStatus
    {
        [Description("创建")]
        Created = 0,
        [Description("启用")]
        Enabled = 1,
        [Description("禁用")]
        Disabled = -1
    }
}
