 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Admin.Application.DictionaryItemService
{
    public class DictionaryItemService : IDictionaryItemService
    {
        private readonly IMapper _mapper;
        private readonly IDictionaryItemRepository _repository;
        private readonly IExcelHandler _excelHandler;
        public DictionaryItemService(IMapper mapper, IDictionaryItemRepository repository, IExcelHandler excelHandler)
        {
            _mapper = mapper;
            _repository = repository;
            _excelHandler = excelHandler;
        }

        public async Task<IResultModel> Query(DictionaryItemQueryModel model)
        {
            var result = new QueryResultModel<DictionaryItemEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictionaryItemAddModel model)
        {
            var entity = _mapper.Map<DictionaryItemEntity>(model);
            var result = await _repository.InsertAsync(entity);
            if (!result)
                return ResultModel.Failed();

            return ResultModel.Success(entity);
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

            var model = _mapper.Map<DictionaryItemUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictionaryItemUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Import(DictionaryItemAddModel model,IFormFile formFile)
        {
            var data = _excelHandler.Import<DictionaryItemImportModel>(formFile);
            var entities = _mapper.Map<List<DictionaryItemEntity>>(data);
            entities.ForEach(m =>
            {
                m.GroupCode = model.GroupCode;
                m.DictCode = model.DictCode;
                m.ParentId = model.ParentId;
            });

            var result = await _repository.InsertRangeAsync(entities);

            return ResultModel.Result(result);
        }

    }
}
