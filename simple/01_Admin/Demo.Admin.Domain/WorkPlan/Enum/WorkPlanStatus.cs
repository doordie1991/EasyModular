using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 工作计划状态
    /// </summary>
    public enum WorkPlanStatus
    {
        [Description("待办")]
        Wait_Do = 0,
        [Description("完成")]
        Finished = 9,
    }
}
