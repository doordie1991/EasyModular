using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum WorkPlanDataType
    {
        [Description("手动")]
        Auto = 0,
        [Description("自动")]
        Manual = 1,
    }
}
