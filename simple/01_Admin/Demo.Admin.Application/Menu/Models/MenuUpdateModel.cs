using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 菜单更新模型
    /// </summary>
    public class MenuUpdateModel : MenuAddModel
    {
        [Required(ErrorMessage = "请选择菜单")]
        public string Id { get; set; }
    }
}
