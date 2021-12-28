using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色用户更新模型
    /// </summary>
    public class RoleUserUpdateModel : RoleUserAddModel
    {
        [Required(ErrorMessage = "请选择角色用户")]
        public string Id { get; set; }
    }
}
