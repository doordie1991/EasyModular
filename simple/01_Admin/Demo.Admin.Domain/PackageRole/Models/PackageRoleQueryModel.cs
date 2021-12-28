
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 套餐角色查询模型
    /// </summary>
    public class PackageRoleQueryModel : QueryPagingModel
    {

        /// <summary>
        /// 套餐Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string PackageId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RoleId { get; set; }
            
    }
}
    