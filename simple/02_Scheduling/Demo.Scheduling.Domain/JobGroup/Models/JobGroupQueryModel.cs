
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Domain
{
    /// <summary>
    /// 任务分组查询模型
    /// </summary>
    public class JobGroupQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }
    }
}
    