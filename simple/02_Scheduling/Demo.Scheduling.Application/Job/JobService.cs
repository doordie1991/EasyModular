
using AutoMapper;
using EasyModular.Utils;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Quartz;
using Quartz;
using Microsoft.Extensions.Logging;

namespace Demo.Scheduling.Application.JobService
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IQuartzServer _quartzServer;
        public JobService(IMapper mapper, ILogger<JobService> logger, IJobRepository jobRepository, IQuartzServer quartzServer)
        {
            _mapper = mapper;
            _logger = logger;
            _jobRepository = jobRepository;
            _quartzServer = quartzServer;
        }

        public async Task<IResultModel> Query(JobQueryModel model)
        {
            var result = new QueryResultModel<JobEntity>
            {
                Rows = await _jobRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobAddModel model)
        {
            var entity = _mapper.Map<JobEntity>(model);
            entity.JobKey = $"{model.JobGroup}.{model.Code}";
            entity.Status = JobStatus.Stop;

            if (await _jobRepository.ExistsAsync(m => m.JobKey == entity.JobKey))
                return ResultModel.Failed($"当前任务组{entity.JobGroup}已存在任务编码${entity.Code}");

            //是否立即启动
            if (model.Start)
            {
                var result = await AddJob(entity);
                if (!result.Successful)
                    return result;

                entity.Status = JobStatus.Running;
            }

            return ResultModel.Result(await _jobRepository.InsertAsync(entity));
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _jobRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var jobKey = new JobKey(entity.Code, entity.JobGroup);
            await _quartzServer.DeleteJob(jobKey);

            var result = await _jobRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _jobRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<JobUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobUpdateModel model)
        {
            var entity = await _jobRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _jobRepository.ExistsAsync(m => m.JobKey == model.JobKey && m.Id != model.Id))
                return ResultModel.Failed($"当前任务组{entity.JobGroup}已存在任务编码${entity.Code}");

            _mapper.Map(model, entity);

            //如果任务不是停止或者已完成，先删除在添加
            if (entity.Status != JobStatus.Stop && entity.Status != JobStatus.Completed)
            {
                var jobKey = new JobKey(entity.Code, entity.JobGroup);
                await _quartzServer.DeleteJob(jobKey);

                var result = await AddJob(entity, entity.Status == JobStatus.Running);
                if (!result.Successful)
                    return result;
            }

            return ResultModel.Result(await _jobRepository.UpdateAsync(entity));
        }

        public async Task<IResultModel> Pause(string id)
        {
            try
            {
                var entity = await _jobRepository.FirstAsync(id);
                if (entity == null)
                    return ResultModel.NotExists;

                if (entity.Status == JobStatus.Stop)
                    return ResultModel.Failed("任务已停止，无法暂停");

                if (entity.Status == JobStatus.Completed)
                    return ResultModel.Failed("任务已完成，无法暂停");

                if (entity.Status == JobStatus.Pause)
                    return ResultModel.Failed("任务已暂停，请刷新页面");


                var jobKey = new JobKey(entity.Code, entity.JobGroup);
                await _quartzServer.PauseJob(jobKey);
            }
            catch (Exception ex)
            {
                _logger.LogError($"暂停任务失败，任务ID:{id}，异常信息:{ex.Message}", ex);
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Resume(string id)
        {
            try
            {
                var entity = await _jobRepository.FirstAsync(id);
                if (entity == null)
                    return ResultModel.NotExists;

                if (entity.Status == JobStatus.Running)
                    return ResultModel.Failed("任务已启动，请刷新页面");

                var jobKey = new JobKey(entity.Code, entity.JobGroup);

                var job = await _quartzServer.GetJob(jobKey);
                if (job == null)
                {
                    if (entity.EndTime <= DateTime.Now)
                        return ResultModel.Failed("任务时效已过期");

                    var result = await AddJob(entity, true);
                    if (!result.Successful)
                        return result;
                }
                else
                {
                    await _quartzServer.ResumeJob(jobKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"重启任务失败，任务ID:{id}，异常信息:{ex.Message}", ex);
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Stop(string id)
        {
            try
            {
                var entity = await _jobRepository.FirstAsync(id);
                if (entity == null)
                    return ResultModel.NotExists;

                if (entity.Status == JobStatus.Stop)
                    return ResultModel.Failed("任务已停止，请刷新页面");

                if (entity.Status == JobStatus.Completed)
                    return ResultModel.Failed("任务已完成，无法停止");


                var jobKey = new JobKey(entity.Code, entity.JobGroup);
                await _quartzServer.DeleteJob(jobKey);

                entity.Status = JobStatus.Stop;
                await _jobRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"停止任务失败，任务ID:{id}，异常信息:{ex.Message}", ex);
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }


        public async Task<IResultModel> UpdateStatus(string group, string code, JobStatus status)
        {
            var entity = await _jobRepository.FirstAsync(m => m.JobGroup == group && m.Code == code && m.IsDel == false);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = status;
            var result = await _jobRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="start">是否立即启动</param>
        /// <returns></returns>
        private async Task<IResultModel> AddJob(JobEntity entity, bool start = false)
        {
            try
            {
                var jobClassType = Type.GetType(entity.JobClass);
                if (jobClassType == null)
                    return ResultModel.Failed($"任务类({entity.JobClass})不存在");

                var jobKey = new JobKey(entity.Code, entity.JobGroup);
                var job = JobBuilder.Create(jobClassType).WithIdentity(jobKey)
                    .UsingJobData("id", entity.Id.ToString()).Build();

                var triggerBuilder = TriggerBuilder.Create().WithIdentity(entity.Code, entity.JobGroup)
                    .EndAt(entity.EndTime.AddDays(1))
                    .WithDescription(entity.Name);

                //如果开始日期小于等于当前日期则立即启动
                if (entity.StartTime <= DateTime.Now)
                    triggerBuilder.StartNow();
                else
                    triggerBuilder.StartAt(entity.StartTime);

                if (entity.TriggerType == TriggerType.Simple)
                {
                    //简单任务
                    triggerBuilder.WithSimpleSchedule(builder =>
                    {
                        builder.WithIntervalInSeconds((int)entity.Interval);
                        if (entity.RepeatCount > 0)
                            builder.WithRepeatCount((int)entity.RepeatCount);
                        else
                            builder.RepeatForever();
                    });
                }
                else
                {
                    if (!CronExpression.IsValidExpression(entity.Cron))
                        return ResultModel.Failed("CRON表达式无效");

                    //CRON任务
                    triggerBuilder.WithCronSchedule(entity.Cron);
                }

                var trigger = triggerBuilder.Build();
                try
                {
                    await _quartzServer.AddJob(job, trigger);

                    if (!start)
                        await _quartzServer.PauseJob(jobKey);  //先暂停

                    return ResultModel.Success();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"任务调度添加任务失败:{ex.Message}", ex);
                }

                return ResultModel.Failed();
            }
            catch (Exception ex)
            {
                _logger.LogError($"添加任务失败:{ex.Message}", ex);
                return ResultModel.Failed(ex.Message);
            }
        }

    }
}
