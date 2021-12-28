using Demo.Scheduling.Application;
using Demo.Scheduling.Application.JobLogService;
using Demo.Scheduling.Domain;
using EasyModular.Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Web
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class TaskLogger : ITaskLogger
    {
        private readonly IJobLogService _service;
        private readonly QuartzOptions _options;

        public TaskLogger(IJobLogService service, QuartzOptions options)
        {
            _service = service;
            _options = options;
        }

        public async Task Info(string jobId,string msg)
        {
            if (!_options.EnabledLogger)
                return;

            var model = new JobLogAddModel
            {
                JobId = jobId,
                Msg = msg,
                Type = JobLogType.Info,
            };

            await _service.Add(model);
        }

        public async Task Debug(string jobId, string msg)
        {
            if (!_options.EnabledLogger)
                return;

            var model = new JobLogAddModel
            {
                JobId = jobId,
                Msg = msg,
                Type = JobLogType.Debug,
            };

            await _service.Add(model);
        }

        public async Task Error(string jobId, string msg)
        {
            if (!_options.EnabledLogger)
                return;

            var model = new JobLogAddModel
            {
                JobId = jobId,
                Msg = msg,
                Type = JobLogType.Error,
            };

            await _service.Add(model);
        }
    }
}
