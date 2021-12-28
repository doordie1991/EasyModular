using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色菜单 更新模型
    /// </summary>
    public class RoleMenuUpdateModel : RoleMenuAddModel
    {
        [Required(ErrorMessage = "请选择角色菜单 ")]
        public string Id { get; set; }
    }
}
