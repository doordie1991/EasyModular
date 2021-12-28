using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Quartz
{
    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public interface ITaskExecutionContext
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        string JobId { get; set; }

        /// <summary>
        /// Quartz的任务执行上下文
        /// </summary>
        IJobExecutionContext JobExecutionContext { get; set; }
    }
}
