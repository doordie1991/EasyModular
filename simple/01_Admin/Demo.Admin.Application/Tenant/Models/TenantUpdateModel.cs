using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户信息更新模型
    /// </summary>
    public class TenantUpdateModel : TenantAddModel
    {
        [Required(ErrorMessage = "请选择租户信息")]
        public string Id { get; set; }
    }
}
