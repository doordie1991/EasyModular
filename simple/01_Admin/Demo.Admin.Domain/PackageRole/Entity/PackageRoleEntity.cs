
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 套餐角色
    /// </summary>
    [SugarTable("Sys_Package_Role", "套餐角色")]
    public partial class PackageRoleEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 是否最高权限角色
        /// </summary>
        public bool? IsTop { get; set; }

    }
}
     