
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 媒体类型查询模型
    /// </summary>
    public class MediaTypeQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 后缀名
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Ext { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Value { get; set; }

    }
}
