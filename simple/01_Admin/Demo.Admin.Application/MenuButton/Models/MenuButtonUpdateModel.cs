using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 菜单按钮更新模型
    /// </summary>
    public class MenuButtonUpdateModel : MenuButtonAddModel
    {
        [Required(ErrorMessage = "请选择菜单按钮")]
        public string Id { get; set; }
    }
}
