using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务日志类型
    /// </summary>
    public enum JobLogType
    {
        [Description("信息")]
        Info,
        [Description("调试")]
        Debug,
        [Description("异常")]
        Error
    }
}
