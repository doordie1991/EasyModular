 
using AutoMapper;
using EasyModular.Utils;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Application.JobLogService
{
    public class JobLogService : IJobLogService
    {
        private readonly IMapper _mapper;
        private readonly IJobLogRepository _repository;
        public JobLogService(IMapper mapper, IJobLogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(JobLogQueryModel model)
        {
            var result = new QueryResultModel<JobLogEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobLogAddModel model)
        {
            var entity = _mapper.Map<JobLogEntity>(model);
            var result = await _repository.InsertAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _repository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _repository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _repository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<JobLogUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobLogUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

    }
}
