using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色菜单权限绑定模型
    /// </summary>
    public class RoleMenuPermissionBindModel
    {
        [Required(ErrorMessage = "请选择角色")]
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<RoleMenuPermissionTreeModel> Menus { get; set; }
    }
}
