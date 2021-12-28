 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Admin.Application.ReleaseLogService
{
    public class ReleaseLogService : IReleaseLogService
    {
        private readonly IMapper _mapper;
        private readonly IReleaseLogRepository _repository;
        public ReleaseLogService(IMapper mapper, IReleaseLogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(ReleaseLogQueryModel model)
        {
            var result = new QueryResultModel<ReleaseLogEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryAll()
        {
            var result = await _repository.GetListAsync(m => m.IsDel == false);
            result.OrderByDescending(m => m.Version);

            return ResultModel.Success(result);
        }


        public async Task<IResultModel> Add(ReleaseLogAddModel model)
        {
            var entity = _mapper.Map<ReleaseLogEntity>(model);
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

            var model = _mapper.Map<ReleaseLogUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ReleaseLogUpdateModel model)
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
