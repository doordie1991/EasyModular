using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    public abstract class TaskAbstract : ITask
    {
        protected readonly ITaskLogger Logger;

        protected TaskAbstract(ITaskLogger logger)
        {
            Logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobId = context.JobDetail.JobDataMap["id"]?.ToString();

            await Logger.Info(jobId, "任务开始");

            try
            {
                await Execute(new TaskExecutionContext
                {
                    JobId = jobId,
                    JobExecutionContext = context
                });
            }
            catch (Exception ex)
            {
                await Logger.Error(jobId, "任务异常：" + ex);
            }

            await Logger.Info(jobId, "任务结束");
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract Task Execute(ITaskExecutionContext context);
    }
}
