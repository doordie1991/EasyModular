
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色权限表查询模型
    /// </summary>
    public class RolePermissionQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RoleId { get; set; }

    }
}
