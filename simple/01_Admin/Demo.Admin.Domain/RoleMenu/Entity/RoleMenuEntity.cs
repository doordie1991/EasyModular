
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色菜单 
    /// </summary>
    [SugarTable("Sys_Role_Menu", "角色菜单")]
    public partial class RoleMenuEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单选中（true;选中；false:半选中）
        /// </summary>
        public bool MenuChecked { get; set; }

    }
}
     