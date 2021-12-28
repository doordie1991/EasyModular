using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典项更新模型
    /// </summary>
    public class DictionaryItemUpdateModel : DictionaryItemAddModel
    {
        [Required(ErrorMessage = "请选择字典项")]
        public string Id { get; set; }
    }
}
