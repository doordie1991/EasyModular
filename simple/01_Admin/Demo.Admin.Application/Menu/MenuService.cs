
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Cache;
using Demo.Admin.Infrastructure;
using System.Linq;

namespace Demo.Admin.Application.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuButtonRepository _menuButtonRepository;
        private readonly ICacheHandler _cacheHandler;
        public MenuService(IMapper mapper
            , DbContext dbContext
            , IMenuRepository menuRepository
            , IMenuButtonRepository menuButtonRepository
            , ICacheHandler cacheHandler)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _menuRepository = menuRepository;
            _menuButtonRepository = menuButtonRepository;
            _cacheHandler = cacheHandler;
        }

        public async Task<IResultModel> Query(MenuQueryModel model)
        {
            var result = new QueryResultModel<MenuEntity>
            {
                Rows = await _menuRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(MenuAddModel model)
        {
            if (!string.IsNullOrEmpty(model.RouteName)
                && await _menuRepository.ExistsAsync(m => m.RouteName == model.RouteName && m.MenuName == model.MenuName && m.IsDel == false))
                return ResultModel.HasExists;

            var entity = _mapper.Map<MenuEntity>(model);
            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                var parent = await _menuRepository.FirstAsync(m => m.Id == entity.ParentId);
                if (parent != null)
                {
                    entity.Level = parent.Level + 1;
                }
            }

            var result = await _menuRepository.InsertAsync(entity);

            if (result)
            {
                await ClearCache();
                return ResultModel.Success(entity);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _menuRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _menuRepository.SoftDeleteAsync(entity);

            if (result)
            {
                await ClearCache();
                return ResultModel.Success(entity);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _menuRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<MenuUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MenuUpdateModel model)
        {
            var entity = await _menuRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (!string.IsNullOrEmpty(model.RouteName) && await _menuRepository.ExistsAsync(m => m.RouteName == model.RouteName && m.MenuName == model.MenuName && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            _mapper.Map(model, entity);

            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                var parent = await _menuRepository.FirstAsync(m => m.Id == entity.ParentId);
                if (parent != null)
                {
                    entity.Level = parent.Level + 1;
                }
            }

            var result = await _menuRepository.UpdateAsync(entity);

            if (result)
            {
                await ClearCache();
                return ResultModel.Success(entity);
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> BindBtn(MenuButtonBindModel model)
        {
            if (!await _menuRepository.ExistsAsync(m => m.Id == model.MenuId))
                return ResultModel.Failed("菜单不存在");

            try
            {
                _dbContext.Db.BeginTran();

                await _menuButtonRepository.DeleteAsync(m => m.MenuId == model.MenuId, _dbContext.Db);

                model.Buttons.ForEach(m => { m.MenuId = model.MenuId; });

                await _menuButtonRepository.InsertRangeAsync(model.Buttons, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Move(MenuMoveModel model)
        {
            var entity = await _menuRepository.FirstAsync(model.SourceId);
            if (entity == null)
                return ResultModel.NotExists;

            if (model.SourceId == model.TargetId)
                return ResultModel.Failed("不能移动至原位置");

            entity.ParentId = model.TargetId;

            var result = await _menuRepository.UpdateAsync(entity);
            await ClearCache();

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> GetTree()
        {
            if (_cacheHandler.TryGetValue(CacheKeys.MENU_TREE, out List<TreeResultModel<string, MenuTreeResultModel>> list))
                return ResultModel.Success(list);

            var all = await _menuRepository.GetListAsync(m => m.IsDel == false);

            list = ResolveTree(all, Guid.Empty.ToString("N"));

            await _cacheHandler.SetAsync(CacheKeys.MENU_TREE, list);

            return ResultModel.Success(list);
        }

        private List<TreeResultModel<string, MenuTreeResultModel>> ResolveTree(IList<MenuEntity> all, string parentId)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort)
                .Select(m => new TreeResultModel<string, MenuTreeResultModel>
                {
                    Id = m.Id,
                    Label = m.MenuName,
                    Item = _mapper.Map<MenuTreeResultModel>(m),
                    Children = ResolveTree(all, m.Id)
                }).ToList();
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        private Task ClearCache()
        {
            var task = _cacheHandler.RemoveAsync(CacheKeys.MENU_TREE);
            return Task.WhenAll(task);
        }
    }
}
