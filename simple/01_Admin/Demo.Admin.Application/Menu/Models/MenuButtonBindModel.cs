using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 菜单按钮绑定模型
    /// </summary>
    public class MenuButtonBindModel
    {
        [Required(ErrorMessage = "请选择菜单")]
        public string MenuId { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public List<MenuButtonEntity> Buttons { get; set; }
    }
}
