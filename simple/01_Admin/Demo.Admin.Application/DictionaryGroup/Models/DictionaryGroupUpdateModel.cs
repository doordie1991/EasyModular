using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典分组更新模型
    /// </summary>
    public class DictionaryGroupUpdateModel : DictionaryGroupAddModel
    {
        [Required(ErrorMessage = "请选择字典分组")]
        public string Id { get; set; }
    }
}
