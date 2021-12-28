using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户类别套餐更新模型
    /// </summary>
    public class TenantTypePackageUpdateModel : TenantTypePackageAddModel
    {
        [Required(ErrorMessage = "请选择租户类别套餐")]
        public string Id { get; set; }
    }
}
