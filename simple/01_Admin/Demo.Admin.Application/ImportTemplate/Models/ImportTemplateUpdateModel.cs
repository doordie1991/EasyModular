using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 导入模板更新模型
    /// </summary>
    public class ImportTemplateUpdateModel : ImportTemplateAddModel
    {
        [Required(ErrorMessage = "请选择导入模板")]
        public string Id { get; set; }
    }
}
