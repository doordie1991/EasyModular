using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 系统配置更新模型
    /// </summary>
    public class ConfigUpdateModel : ConfigAddModel
    {
        [Required(ErrorMessage = "请选择系统配置")]
        public string Id { get; set; }
    }
}
