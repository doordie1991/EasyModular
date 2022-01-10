
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务
    /// </summary>
    [SugarTable("Sch_Job", "任务")]
    public partial class JobEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务唯一键
        /// </summary>
        public string JobKey { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 任务执行类
        /// </summary>
        public string JobClass { get; set; }

        /// <summary>
        /// 触发类型(0.简单触发器、1.CRON触发器)
        /// </summary>
        public TriggerType TriggerType { get; set; }

        /// <summary>
        /// 简单触发器时间间隔(秒)
        /// </summary>
        public int? Interval { get; set; }

        /// <summary>
        /// 简单触发器重复次数，0表示无限
        /// </summary>
        public int? RepeatCount { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 状态（0.Stop、1.运行、2.暂停、3.已完成、4.异常）
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
     