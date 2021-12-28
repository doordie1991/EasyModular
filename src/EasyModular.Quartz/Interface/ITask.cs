using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITask : IJob
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task Execute(ITaskExecutionContext context);
    }
}
