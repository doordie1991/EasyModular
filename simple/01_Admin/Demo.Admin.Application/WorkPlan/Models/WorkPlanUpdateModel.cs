using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 工作计划更新模型
    /// </summary>
    public class WorkPlanUpdateModel : WorkPlanAddModel
    {
        [Required(ErrorMessage = "请选择工作计划")]
        public string Id { get; set; }
    }
}
