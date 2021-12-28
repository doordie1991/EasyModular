using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典更新模型
    /// </summary>
    public class DictionaryUpdateModel : DictionaryAddModel
    {
        [Required(ErrorMessage = "请选择字典")]
        public string Id { get; set; }
    }
}
