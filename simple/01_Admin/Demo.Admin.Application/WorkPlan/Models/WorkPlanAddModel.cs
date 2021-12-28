using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 工作计划新增模型
    /// </summary>
    public class WorkPlanAddModel
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


    }
}
