
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 工作计划
    /// </summary>
    [SugarTable("Sys_WorkPlan", "工作计划")]
    public partial class WorkPlanEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 计划事项
        /// </summary>
        public string PlanItem { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public WorkPlanStatus Status { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public WorkPlanDataType DataType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
     