using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    public class TaskLogger : ITaskLogger
    {
        private readonly ILogger _logger;

        public TaskLogger(ILogger<TaskLogger> logger)
        {
            _logger = logger;
        }

        public Task Info(string jobId,string msg)
        {
            _logger.LogInformation($"任务编号:{jobId}, {msg}");
            return Task.CompletedTask;
        }

        public Task Debug(string jobId, string msg)
        {
            _logger.LogDebug($"任务编号:{jobId}, {msg}");
            return Task.CompletedTask;
        }

        public Task Error(string jobId, string msg)
        {
            _logger.LogError($"任务编号:{jobId}, {msg}");
            return Task.CompletedTask;
        }
    }
}
