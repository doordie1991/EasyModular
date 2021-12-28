
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Admin.Infrastructure;
using System.Linq;

namespace Demo.Admin.Application.DictionaryService
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IMapper _mapper;
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IDictionaryItemRepository _dictionaryItemRepository;
        public DictionaryService(IMapper mapper, IDictionaryRepository dictionaryRepository, IDictionaryItemRepository dictionaryItemRepository)
        {
            _mapper = mapper;
            _dictionaryRepository = dictionaryRepository;
            _dictionaryItemRepository = dictionaryItemRepository;
        }

        public async Task<IResultModel> Query(DictionaryQueryModel model)
        {
            var result = new QueryResultModel<DictionaryEntity>
            {
                Rows = await _dictionaryRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(DictionaryAddModel model)
        {
            if (await _dictionaryRepository.ExistsAsync(m => m.GroupCode == model.GroupCode && m.Code == model.Code && m.IsDel == false))
                return ResultModel.HasExists;

            var entity = _mapper.Map<DictionaryEntity>(model);
            var result = await _dictionaryRepository.InsertAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _dictionaryRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _dictionaryRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _dictionaryRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<DictionaryUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(DictionaryUpdateModel model)
        {
            var entity = await _dictionaryRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _dictionaryRepository.ExistsAsync(m => m.GroupCode == model.GroupCode && m.Code == model.Code && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            _mapper.Map(model, entity);

            var result = await _dictionaryRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            var list = await _dictionaryItemRepository.GetListAsync(m => m.GroupCode == group && m.DictCode == code && m.IsDel == false);
            var result = list.OrderBy(m => m.Sort).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Value,
                Data = new
                {
                    m.Id,
                    m.Name,
                    m.Value,
                    m.Extend,
                    m.Icon,
                    m.Level
                }
            }).ToList();

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Tree(string group, string code)
        {
            if (group.IsNull() || code.IsNull())
                return ResultModel.Failed("请指定分组和编码");

            var dict = await _dictionaryRepository.FirstAsync(m => m.GroupCode == group && m.Code == code && m.IsDel == false);
            if (dict == null)
                return ResultModel.Failed("字典不存在");

            var root = new TreeResultModel<string, DictionaryItemTreeResultModel>
            {
                Id = dict.Id,
                Label = dict.Name,
                Item = new DictionaryItemTreeResultModel
                {
                    Id = dict.Id,
                    Name = dict.Name
                }
            };

            var all = await _dictionaryItemRepository.GetListAsync(m => m.GroupCode == group && m.DictCode == code && m.IsDel == false);
            root.Children = ResolveTree(all, dict.Id);


            return ResultModel.Success(root);
        }

        private List<TreeResultModel<string, DictionaryItemTreeResultModel>> ResolveTree(IList<DictionaryItemEntity> all, string parentId)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort).Select(m =>
            {
                var node = new TreeResultModel<string, DictionaryItemTreeResultModel>
                {
                    Id = m.Id,
                    Label = m.Name,
                    Value = m.Value,
                    Item = _mapper.Map<DictionaryItemTreeResultModel>(m),
                    Children = ResolveTree(all, m.Id)
                };

                return node;
            }).ToList();
        }

    }
}
