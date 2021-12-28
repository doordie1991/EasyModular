
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 发布记录查询模型
    /// </summary>
    public class ReleaseLogQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 版本
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Version { get; set; }

        /// <summary>
        /// 发布内容
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Content { get; set; }

    }
}
