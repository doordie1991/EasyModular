using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 发布记录更新模型
    /// </summary>
    public class ReleaseLogUpdateModel : ReleaseLogAddModel
    {
        [Required(ErrorMessage = "请选择发布记录")]
        public string Id { get; set; }
    }
}
