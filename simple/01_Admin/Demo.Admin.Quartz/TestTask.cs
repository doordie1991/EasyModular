using Demo.Admin.Application.UserService;
using EasyModular.Quartz;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Quartz
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestTask: TaskAbstract
    {
        public TestTask(ITaskLogger logger) : base(logger)
        {
          
        }

        public override Task Execute(ITaskExecutionContext context)
        {
            Console.WriteLine(Guid.NewGuid().ToString());
            return Task.CompletedTask;
        }
    }
}
