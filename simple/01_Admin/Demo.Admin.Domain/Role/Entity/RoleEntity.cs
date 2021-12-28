
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    [SugarTable("Sys_Role", "角色")]
    public partial class RoleEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
     