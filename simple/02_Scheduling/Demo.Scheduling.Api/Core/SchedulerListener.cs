using Demo.Scheduling.Application.JobService;
using Demo.Scheduling.Domain;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Scheduling.Web
{
    public class SchedulerListener : ISchedulerListener
    {
        private readonly IJobService _service;

        public SchedulerListener(IJobService service)
        {
            _service = service;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            return _service.UpdateStatus(jobDetail.Key.Group, jobDetail.Key.Name, JobStatus.Running);
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return _service.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Completed);
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //暂停
            return _service.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Pause);
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            //恢复
            return _service.UpdateStatus(jobKey.Group, jobKey.Name, JobStatus.Running);
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }


        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
