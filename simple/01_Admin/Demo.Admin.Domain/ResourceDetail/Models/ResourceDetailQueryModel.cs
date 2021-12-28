
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源明细查询模型
    /// </summary>
    public class ResourceDetailQueryModel : QueryPagingModel
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

        /// <summary>
        /// 字段类型
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string FieldType { get; set; }
    }
}
    