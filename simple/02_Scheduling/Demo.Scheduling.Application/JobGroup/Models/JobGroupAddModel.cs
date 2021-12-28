using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Scheduling.Application
{
    /// <summary>
    /// 任务分组新增模型
    /// </summary>
    public class JobGroupAddModel
    {
        
        /// <summary>
        /// 分组编码
        /// </summary>
        public string Code  { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }
    }
}
