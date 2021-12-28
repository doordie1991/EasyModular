using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色更新模型
    /// </summary>
    public class RoleUpdateModel : RoleAddModel
    {
        [Required(ErrorMessage = "请选择角色")]
        public string Id { get; set; }
    }
}
