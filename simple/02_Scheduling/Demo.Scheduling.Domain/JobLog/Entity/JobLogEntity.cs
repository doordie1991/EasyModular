
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务日志
    /// </summary>
    [SugarTable("Sch_Job_Log", "任务日志")]
    public partial class JobLogEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 任务编号
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// 任务日志类型（0.信息、1.调试、2.异常）
        /// </summary>
        public JobLogType Type { get; set; }

        /// <summary>
        /// 内容信息
        /// </summary>
        public string Msg { get; set; }

    }
}
     