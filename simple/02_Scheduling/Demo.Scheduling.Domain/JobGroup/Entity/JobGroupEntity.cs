
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务分组
    /// </summary>
    [SugarTable("Sch_Job_Group", "任务分组")]
    public partial class JobGroupEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 分组编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

    }
}
     