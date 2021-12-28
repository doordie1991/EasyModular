
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务日志查询模型
    /// </summary>
    public class JobLogQueryModel : QueryPagingModel
    {

        /// <summary>
        /// 任务编号
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string JobId { get; set; }

        /// <summary>
        /// 任务日志类型（0.信息、1.调试、2.异常）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal, ConvertType = ConvertType.ToEnum)]
        public JobLogType Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Condition(FieldName = "CreatedTime", ConditionalType = ConditionalType.GreaterThanOrEqual)]
        public DateTime? StartTime { get; set; }


        private DateTime? endTime;
        /// <summary>
        /// 结束时间
        /// </summary> 
        [Condition(FieldName = "CreatedTime", ConditionalType = ConditionalType.LessThan)]
        public DateTime? EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (value != null) endTime = Convert.ToDateTime(value.ToString()).AddDays(1);
            }
        }
    }
}
