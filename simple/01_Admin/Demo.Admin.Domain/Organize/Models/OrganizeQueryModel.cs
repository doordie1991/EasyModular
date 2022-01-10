
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 组织架构表查询模型
    /// </summary>
    public class OrganizeQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeName { get; set; }

        /// <summary>
        /// 组织全称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeFullName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string ParentId { get; set; }

    }
}
