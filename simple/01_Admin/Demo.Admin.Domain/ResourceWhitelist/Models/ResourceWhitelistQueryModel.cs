
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源白名单查询模型
    /// </summary>
    public class ResourceWhitelistQueryModel : QueryPagingModel
    {

        /// <summary>
        /// 资源Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string ResourceId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Value { get; set; }

        /// <summary>
        /// 值类别（0.用户、1.角色、2.组织）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public ValueType? ValueType { get; set; }
    }
}
    