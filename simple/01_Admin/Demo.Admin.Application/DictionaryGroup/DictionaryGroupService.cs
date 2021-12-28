
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Admin.Application.DictionaryGroupService
{
    public class DictionaryGroupService : IDictionaryGroupService
    {
        private readonly IMapper _mapper;
        private readonly IDictionaryGroupRepository _repository;
        public DictionaryGroupService(IMapper mapper, IDictionaryGroupRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(DictionaryGroupQueryModel model)
        {
            var result = new QueryResultModel<DictionaryGroupEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictionaryGroupAddModel model)
        {
            if (await _repository.ExistsAsync(m => m.Code == model.Code && m.IsDel == false))
                return ResultModel.HasExists;

            var entity = _mapper.Map<DictionaryGroupEntity>(model);
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

            var model = _mapper.Map<DictionaryGroupUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictionaryGroupUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _repository.ExistsAsync(m => m.Code == model.Code && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var list = await _repository.GetListAsync(m => m.IsDel == false);
            var result = list.OrderBy(m => m.Sort).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            });

            return ResultModel.Success(result);
        }

    }
}
