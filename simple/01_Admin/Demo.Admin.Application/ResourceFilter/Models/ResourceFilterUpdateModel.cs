using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源过滤更新模型
    /// </summary>
    public class ResourceFilterUpdateModel : ResourceFilterAddModel
    {
        [Required(ErrorMessage = "请选择资源过滤")]
        public string Id { get; set; }
    }
}
