 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.TenantTypePackageService
{
    public class TenantTypePackageService : ITenantTypePackageService
    {
        private readonly IMapper _mapper;
        private readonly ITenantTypePackageRepository _repository;
        public TenantTypePackageService(IMapper mapper, ITenantTypePackageRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(TenantTypePackageQueryModel model)
        {
            var result = new QueryResultModel<TenantTypePackageEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(TenantTypePackageAddModel model)
        {
            var entity = _mapper.Map<TenantTypePackageEntity>(model);
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

            var model = _mapper.Map<TenantTypePackageUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(TenantTypePackageUpdateModel model)
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
