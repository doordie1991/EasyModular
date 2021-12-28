using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Quartz
{
    /// <summary>
    /// 调度模块描述信息
    /// </summary>
    public class QuartzModuleDescriptor
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        public IModuleDescriptor Module { get; set; }

        /// <summary>
        /// 任务列表
        /// </summary>
        public List<QuartzTaskDescriptor> Tasks { get; } = new List<QuartzTaskDescriptor>();

    }
}
