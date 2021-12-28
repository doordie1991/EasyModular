
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源过滤查询模型
    /// </summary>
    public class ResourceFilterQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string ResourceId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string FieldName { get; set; }

    }
}
    