 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Demo.Admin.Application.ResourceDetailService
{
    public class ResourceDetailService : IResourceDetailService
    {
        private readonly IMapper _mapper;
        private readonly IResourceDetailRepository _repository;
        public ResourceDetailService(IMapper mapper, IResourceDetailRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IResultModel> Query(ResourceDetailQueryModel model)
        {
            var result = new QueryResultModel<ResourceDetailEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ResourceDetailAddModel model)
        {
            var entity = _mapper.Map<ResourceDetailEntity>(model);
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

            var model = _mapper.Map<ResourceDetailUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ResourceDetailUpdateModel model)
        {
            var entity = await _repository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _repository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string resourceId)
        {
            if (resourceId.IsNull())
                return ResultModel.Failed("请指定自定资源");

            var list = await _repository.GetListAsync(m => m.ResourceId == resourceId && m.IsDel == false);
            var result = list.OrderBy(m => m.CreatedTime).Select(m => new OptionResultModel
            {
                Label = m.FieldName,
                Value = m.FieldName,
                Data = new
                {
                    m.Id,
                    m.FieldName,
                    m.FieldType
                }
            }).ToList();

            return ResultModel.Success(result);
        }

    }
}
