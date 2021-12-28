using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户类别更新模型
    /// </summary>
    public class TenantTypeUpdateModel : TenantTypeAddModel
    {
        [Required(ErrorMessage = "请选择租户类别")]
        public string Id { get; set; }
    }
}
