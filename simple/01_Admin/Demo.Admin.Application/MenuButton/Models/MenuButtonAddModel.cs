using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 菜单按钮新增模型
    /// </summary>
    public class MenuButtonAddModel
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
