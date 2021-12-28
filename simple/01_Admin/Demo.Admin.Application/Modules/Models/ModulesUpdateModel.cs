using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 模块更新模型
    /// </summary>
    public class ModulesUpdateModel : ModulesAddModel
    {
        [Required(ErrorMessage = "请选择模块")]
        public string Id { get; set; }
    }
}
