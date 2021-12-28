using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 系统皮肤更新模型
    /// </summary>
    public class SkinUpdateModel : SkinAddModel
    {
        [Required(ErrorMessage = "请选择系统皮肤")]
        public string Id { get; set; }
    }
}
