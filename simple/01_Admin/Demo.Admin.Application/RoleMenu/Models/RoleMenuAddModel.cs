using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色菜单 新增模型
    /// </summary>
    public class RoleMenuAddModel
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
