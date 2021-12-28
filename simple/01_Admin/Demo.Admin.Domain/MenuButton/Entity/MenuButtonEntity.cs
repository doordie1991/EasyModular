
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 菜单按钮
    /// </summary>
    [SugarTable("Sys_Menu_Button", "菜单按钮")]
    public partial class MenuButtonEntity : SoftDeleteEntity<string>
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
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        public string IconColor { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public string MenuId { get; set; }

    }
}
     