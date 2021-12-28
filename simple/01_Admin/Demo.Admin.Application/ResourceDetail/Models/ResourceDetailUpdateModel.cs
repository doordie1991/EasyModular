using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源明细更新模型
    /// </summary>
    public class ResourceDetailUpdateModel : ResourceDetailAddModel
    {
        [Required(ErrorMessage = "请选择资源明细")]
        public string Id { get; set; }
    }
}
