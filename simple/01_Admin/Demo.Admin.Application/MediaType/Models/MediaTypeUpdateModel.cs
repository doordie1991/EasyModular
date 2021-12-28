using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 媒体类型更新模型
    /// </summary>
    public class MediaTypeUpdateModel : MediaTypeAddModel
    {
        [Required(ErrorMessage = "请选择媒体类型")]
        public string Id { get; set; }
    }
}
