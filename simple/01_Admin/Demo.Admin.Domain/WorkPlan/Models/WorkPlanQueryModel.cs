
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 工作计划查询模型
    /// </summary>
    public class WorkPlanQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 计划名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string PlanName { get; set; }

        /// <summary>
        /// 计划事项
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string PlanItem { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        [Condition(FieldName = "PlanStartTime", ConditionalType = ConditionalType.GreaterThanOrEqual)]
        public DateTime? PlanStartTime { get; set; }

        private DateTime? planEndTime;
        /// <summary>
        /// 计划结束时间
        /// </summary> 
        [Condition(FieldName = "PlanStartTime", ConditionalType = ConditionalType.GreaterThanOrEqual)]
        public DateTime? PlanEndTime
        {
            get
            {
                return planEndTime;
            }
            set
            {
                if (value != null) planEndTime = Convert.ToDateTime(value.ToString()).AddDays(1);
            }
        }

        /// <summary>
        /// 状态（0:待办；9::完成）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public WorkPlanStatus? Status { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public WorkPlanDataType? DataType { get; set; }

    }
}
