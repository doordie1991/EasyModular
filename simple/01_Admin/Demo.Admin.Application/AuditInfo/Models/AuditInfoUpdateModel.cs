using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 审计信息更新模型
    /// </summary>
    public class AuditInfoUpdateModel : AuditInfoAddModel
    {
        [Required(ErrorMessage = "请选择审计信息")]
        public string Id { get; set; }
    }
}
