using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 套餐角色更新模型
    /// </summary>
    public class PackageRoleUpdateModel : PackageRoleAddModel
    {
        [Required(ErrorMessage = "请选择套餐角色")]
        public string Id { get; set; }
    }
}
