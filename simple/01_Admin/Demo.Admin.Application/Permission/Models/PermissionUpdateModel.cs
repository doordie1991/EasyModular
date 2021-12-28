using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 权限更新模型
    /// </summary>
    public class PermissionUpdateModel : PermissionAddModel
    {
        [Required(ErrorMessage = "请选择权限")]
        public string Id { get; set; }
    }
}
