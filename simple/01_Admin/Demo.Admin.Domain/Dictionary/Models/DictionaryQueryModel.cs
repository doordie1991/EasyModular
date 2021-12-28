
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典表查询模型
    /// </summary>
    public class DictionaryQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string GroupCode { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string GroupName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }

    }
}
