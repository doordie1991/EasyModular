using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色权限更新模型
    /// </summary>
    public class RolePermissionUpdateModel : RolePermissionAddModel
    {
        [Required(ErrorMessage = "请选择角色权限")]
        public string Id { get; set; }
    }
}
