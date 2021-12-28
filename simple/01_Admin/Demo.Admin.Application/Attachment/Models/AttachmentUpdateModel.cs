using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 附件更新模型
    /// </summary>
    public class AttachmentUpdateModel : AttachmentAddModel
    {
        [Required(ErrorMessage = "请选择附件")]
        public string Id { get; set; }
    }
}
