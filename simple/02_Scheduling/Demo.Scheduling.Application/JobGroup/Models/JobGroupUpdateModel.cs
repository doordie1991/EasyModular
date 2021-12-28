using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Scheduling.Application
{
    /// <summary>
    /// 任务分组更新模型
    /// </summary>
    public class JobGroupUpdateModel : JobGroupAddModel
    {
        [Required(ErrorMessage = "请选择任务分组")]
        public string Id { get; set; }
    }
}
