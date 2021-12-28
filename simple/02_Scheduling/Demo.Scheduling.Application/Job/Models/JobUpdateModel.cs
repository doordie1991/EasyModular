using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Scheduling.Application
{
    /// <summary>
    /// 任务更新模型
    /// </summary>
    public class JobUpdateModel : JobAddModel
    {
        [Required(ErrorMessage = "请选择任务")]
        public string Id { get; set; }
    }
}
