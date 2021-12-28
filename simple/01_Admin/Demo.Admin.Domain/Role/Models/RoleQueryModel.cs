
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色表查询模型
    /// </summary>
    public class RoleQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 角色编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string RoleName { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string TenantId { get; set; }

    }
}
