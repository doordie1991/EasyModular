using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Scheduling.Application
{
    /// <summary>
    /// 任务日志更新模型
    /// </summary>
    public class JobLogUpdateModel : JobLogAddModel
    {
        [Required(ErrorMessage = "请选择任务日志")]
        public string Id { get; set; }
    }
}
