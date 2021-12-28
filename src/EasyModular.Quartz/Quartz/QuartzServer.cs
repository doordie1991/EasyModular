using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    public class QuartzServer : IQuartzServer
    {
        private IScheduler _scheduler;
        private readonly ILogger _logger;
        private readonly IServiceProvider _container;

        public QuartzServer(ILogger<QuartzServer> logger, IServiceProvider container)
        {
            _logger = logger;
            _container = container;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public async Task Start(CancellationToken cancellation = default)
        {
            //调度器工厂
            var props = GetQuartzProps();
            var factory = new StdSchedulerFactory(props);

            //创建一个调度器
            _scheduler = await factory.GetScheduler(cancellation);

            //绑定自定义任务工厂
            _scheduler.JobFactory = new JobFactory(_container);

            //添加任务监听器
            AddJobListener();

            //添加触发器监听器
            AddTriggerListener();

            //添加调度服务监听器
            AddSchedulerListener();

            //启动
            await _scheduler.Start(cancellation);

            _logger.LogInformation("Quartz server started");
        }


        /// <summary>
        /// 停止
        /// </summary>
        public async Task Stop(CancellationToken cancellation = default)
        {
            if (_scheduler == null)
                return;

            await _scheduler.Shutdown(true, cancellation);

            _logger.LogInformation("Quartz server stopped");
        }


        /// <summary>
        /// 添加任务
        /// </summary>
        public Task<IJobDetail> GetJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.GetJobDetail(jobKey, cancellation);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        public Task AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default)
        {
            return _scheduler.ScheduleJob(jobDetail, trigger, cancellation);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        public Task PauseJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.PauseJob(jobKey, cancellation);
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        public Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.ResumeJob(jobKey, cancellation);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        public Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.DeleteJob(jobKey, cancellation);
        }

        #region ==私有方法==

        /// <summary>
        /// 添加调度服务监听器
        /// </summary>
        private void AddSchedulerListener()
        {
            var schedulerListeners = _container.GetServices<ISchedulerListener>().ToList();

            foreach (var listener in schedulerListeners)
                _scheduler.ListenerManager.AddSchedulerListener(listener);
        }

        /// <summary>
        /// 添加任务监听器
        /// </summary>
        private void AddJobListener()
        {
            var jobListeners = _container.GetServices<IJobListener>().ToList();

            foreach (var listener in jobListeners)
                _scheduler.ListenerManager.AddJobListener(listener);
        }

        /// <summary>
        /// 添加触发器监听器
        /// </summary>
        private void AddTriggerListener()
        {
            var triggerListener = _container.GetServices<ITriggerListener>().ToList();

            foreach (var listener in triggerListener)
                _scheduler.ListenerManager.AddTriggerListener(listener);
        }


        private NameValueCollection GetQuartzProps()
        {
            var opts= _container.GetServices<QuartzOptions>().FirstOrDefault();

            var quartzProps = new NameValueCollection();
            quartzProps["quartz.scheduler.instanceName"] = opts.Scheduler.InstanceName;
            quartzProps["quartz.jobStore.type"] = opts.JobStore.Type;
            quartzProps["quartz.jobStore.driverDelegateType"] = opts.JobStore.DriverDelegateType;
            quartzProps["quartz.jobStore.tablePrefix"] = opts.JobStore.TablePrefix;
            quartzProps["quartz.jobStore.dataSource"] = opts.JobStore.DataSource;
            quartzProps["quartz.dataSource.default.connectionString"] = opts.JobStore.ConnectionString;
            quartzProps["quartz.dataSource.default.provider"] = opts.JobStore.Provider;
            quartzProps["quartz.serializer.type"] = "json";
            return quartzProps;

        }
        #endregion
    }
}
