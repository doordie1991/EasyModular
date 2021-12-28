 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Admin.Application.ImportTemplateService
{
    public class ImportTemplateService : IImportTemplateService
    {
        private readonly IMapper _mapper;
        private readonly IImportTemplateRepository _repository;
        public ImportTemplateService(IMapper mapper, IImportTemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(ImportTemplateQueryModel model)
        {
            var result = new QueryResultModel<ImportTemplateEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByCode(string moduleCode,string templateCode)
        {
            var entity = await _repository.FirstAsync(m => m.ModuleCode == moduleCode && m.TemplateCode == templateCode);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<ImportTemplateUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Add(ImportTemplateAddModel model)
        {
            var entity = _mapper.Map<ImportTemplateEntity>(model);
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

            var model = _mapper.Map<ImportTemplateUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ImportTemplateUpdateModel model)
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
