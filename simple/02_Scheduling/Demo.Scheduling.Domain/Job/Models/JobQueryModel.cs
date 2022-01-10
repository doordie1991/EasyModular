
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务查询模型
    /// </summary>
    public class JobQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 模块
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string Module { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务唯一键
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string JobKey { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }

        /// <summary>
        /// 状态（0.Stop、1.运行、2.暂停、3.已完成、4.异常）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal, ConvertType = ConvertType.ToEnum)]
        public JobStatus? Status { get; set; }
    }
}
