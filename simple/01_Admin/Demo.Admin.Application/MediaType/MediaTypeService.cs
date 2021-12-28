 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Demo.Admin.Application.MediaTypeService
{
    public class MediaTypeService : IMediaTypeService
    {
        private readonly IMapper _mapper;
        private readonly IMediaTypeRepository _repository;
        private readonly IExcelHandler _excelHandler;
        public MediaTypeService(IMapper mapper, IMediaTypeRepository repository, IExcelHandler excelHandler)
        {
            _mapper = mapper;
            _repository = repository;
            _excelHandler = excelHandler;
        }

        public async Task<IResultModel> Query(MediaTypeQueryModel model)
        {
            var result = new QueryResultModel<MediaTypeEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(MediaTypeAddModel model)
        {
            var entity = _mapper.Map<MediaTypeEntity>(model);
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

            var model = _mapper.Map<MediaTypeUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MediaTypeUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Import(IFormFile formFile)
        {
            var oldData = await _repository.GetListAsync(m => m.IsDel == false);

            var data = _excelHandler.Import<MediaTypeImportModel>(formFile);
            var entities = _mapper.Map<List<MediaTypeEntity>>(data)
                                  .Where(m=> !oldData.Select(m => m.Ext).Contains(m.Ext)).ToList();

            var result = await _repository.InsertRangeAsync(entities);

            return ResultModel.Result(result);
        }

    }
}
