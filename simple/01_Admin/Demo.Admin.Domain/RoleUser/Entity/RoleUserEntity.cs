
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色用户
    /// </summary>
    [SugarTable("Sys_Role_User", "角色用户")]
    public partial class RoleUserEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

    }
}
     