using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Quartz
{
    public class TaskExecutionContext : ITaskExecutionContext
    {
        public string JobId { get; set; }

        public IJobExecutionContext JobExecutionContext { get; set; }
    }
}
