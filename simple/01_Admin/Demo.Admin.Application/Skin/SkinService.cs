
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Auth;

namespace Demo.Admin.Application.SkinService
{
    public class SkinService : ISkinService
    {
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly ISkinRepository _repository;
        public SkinService(IMapper mapper, ILoginInfo loginInfo, ISkinRepository repository)
        {
            _mapper = mapper;
            _loginInfo = loginInfo;
            _repository = repository;
        }

        public async Task<IResultModel> Query(SkinQueryModel model)
        {
            var result = new QueryResultModel<SkinEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(SkinAddModel model)
        {
            var entity = _mapper.Map<SkinEntity>(model);
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

            var model = _mapper.Map<SkinUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(SkinUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Save(SkinAddModel model)
        {
            model.UserId = _loginInfo.UserId;
            var entity = await _repository.FirstAsync(m => m.UserId == model.UserId);
            if (entity == null)
            {
                entity = _mapper.Map<SkinEntity>(model);
                var result = await _repository.InsertAsync(entity);
                return ResultModel.Result(result);
            }
            else
            {
                _mapper.Map(model, entity);
                var result = await _repository.UpdateAsync(entity);
                return ResultModel.Result(result);
            }
        }

    }
}
