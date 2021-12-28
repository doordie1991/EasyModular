using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 套餐更新模型
    /// </summary>
    public class PackageUpdateModel : PackageAddModel
    {
        [Required(ErrorMessage = "请选择套餐")]
        public string Id { get; set; }
    }
}
