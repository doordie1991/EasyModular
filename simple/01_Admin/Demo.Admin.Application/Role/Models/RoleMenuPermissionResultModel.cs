using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色菜单的权限返回模型
    /// </summary>
    public class RoleMenuPermissionResultModel
    {
        /// <summary>
        /// 菜单树形列表
        /// </summary>
        public List<TreeResultModel<string, RoleMenuPermissionTreeModel>>  Menus { get; set; }

        /// <summary>
        /// 角色菜单Id集合
        /// </summary>
        public IList<string> RoleMenuIds { get; set; }
    }
}
