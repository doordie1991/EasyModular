
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典项查询模型
    /// </summary>
    public class DictionaryItemQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 父节点Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string ParentId { get; set; }

        /// <summary>
        /// 分组编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string DictCode { get; set; }

        /// <summary>
        /// 字典项编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 字典项名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string ItemValue { get; set; }

    }
}
