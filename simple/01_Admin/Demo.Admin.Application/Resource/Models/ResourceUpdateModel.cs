using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源更新模型
    /// </summary>
    public class ResourceUpdateModel : ResourceAddModel
    {
        [Required(ErrorMessage = "请选择资源")]
        public string Id { get; set; }
    }
}
