using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色菜单权限树形模型
    /// </summary>
    public class RoleMenuPermissionTreeModel
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单选中（true;选中；false:半选中）
        /// </summary>
        public bool MenuChecked { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public List<RoleMenuButtons> Buttons { get; set; }

    }

    public class RoleMenuButtons
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { get; set; }
    }
}
