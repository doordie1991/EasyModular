using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 企业状态
    /// </summary>
    public enum TenantStatus
    {
        [Description("创建")]
        Created = 0,
        [Description("启用")]
        Enabled = 1,
        [Description("禁用")]
        Disabled = -1
    }
}
