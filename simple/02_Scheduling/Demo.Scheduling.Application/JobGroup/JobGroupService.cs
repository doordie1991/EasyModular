 
using AutoMapper;
using EasyModular.Utils;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Scheduling.Application.JobGroupService
{
    public class JobGroupService : IJobGroupService
    {
        private readonly IMapper _mapper;
        private readonly IJobGroupRepository _repository;
        public JobGroupService(IMapper mapper, IJobGroupRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(JobGroupQueryModel model)
        {
            var result = new QueryResultModel<JobGroupEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobGroupAddModel model)
        {
            var entity = _mapper.Map<JobGroupEntity>(model);
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

            var model = _mapper.Map<JobGroupUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobGroupUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }


        public async Task<IResultModel> Select()
        {

            var list = await _repository.GetListAsync(m => m.IsDel == false);
            var result = list.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            }).ToList();

            return ResultModel.Success(result);
        }

    }
}
