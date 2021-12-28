using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 触发器类型
    /// </summary>
    public enum TriggerType
    {
        [Description("简单触发器")]
        Simple,
        [Description("CRON触发器")]
        Cron
    }
}
