
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
using EasyModular;
using EasyModular.Auth;

namespace Demo.Admin.Application.OrganizeService
{
    public class OrganizeService : IOrganizeService
    {
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly IOrganizeRepository _organizeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICacheHandler _cacheHandler;
        public OrganizeService(IMapper mapper, ILoginInfo loginInfo, IOrganizeRepository organizeRepository, IUserRepository userRepository, ICacheHandler cacheHandler)
        {
            _mapper = mapper;
            _loginInfo = loginInfo;
            _organizeRepository = organizeRepository;
            _userRepository = userRepository;
            _cacheHandler = cacheHandler;
        }

        public async Task<IResultModel> Query(OrganizeQueryModel model)
        {
            var result = new QueryResultModel<OrganizeEntity>
            {
                Rows = await _organizeRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(OrganizeAddModel model)
        {
            if (await _organizeRepository.ExistsAsync(m => (m.OrganizeCode == model.OrganizeCode || m.OrganizeName == model.OrganizeName) && m.IsDel == false))
                return ResultModel.HasExists;

            var entity = _mapper.Map<OrganizeEntity>(model);

            if (entity.ParentId == Guid.Empty.ToString("N"))
            {
                entity.OrganizeFullName = $"{entity.OrganizeName}";
            }
            else
            {
                var parent = await _organizeRepository.FirstAsync(m => m.Id == entity.ParentId);
                if (parent != null)
                {
                    entity.Level = parent.Level + 1;
                    entity.OrganizeFullName = $"{parent.OrganizeFullName}/{entity.OrganizeName}";
                }
            }

            var result = await _organizeRepository.InsertAsync(entity);
            if (result)
                return ResultModel.Success(entity);

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _organizeRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _userRepository.ExistsAsync(m => m.OrganizeId == id))
                return ResultModel.Failed("当前组织包含用户，请先删除用户");

            var result = await _organizeRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _organizeRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<OrganizeUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(OrganizeUpdateModel model)
        {
            var entity = await _organizeRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            if (await _organizeRepository.ExistsAsync(m => (m.OrganizeCode == model.OrganizeCode || m.OrganizeName == model.OrganizeName) && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            if (entity.ParentId == Guid.Empty.ToString("N"))
            {
                entity.OrganizeFullName = $"{entity.OrganizeName}";
            }
            else
            {
                var parent = await _organizeRepository.FirstAsync(m => m.Id == entity.ParentId);
                if (parent != null)
                {
                    entity.Level = parent.Level + 1;
                    entity.OrganizeFullName = $"{parent.OrganizeFullName}/{entity.OrganizeName}";
                }
            }

            var result = await _organizeRepository.UpdateAsync(entity);

            if (result)
                return ResultModel.Success(entity);

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Move(OrganizeMoveModel model)
        {
            var entity = await _organizeRepository.FirstAsync(model.SourceId);
            if (entity == null)
                return ResultModel.NotExists;

            if (model.SourceId == model.TargetId)
                return ResultModel.Failed("不能移动至原位置");

            entity.ParentId = model.TargetId;

            var result = await _organizeRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> GetTree()
        {
            var list = new List<TreeResultModel<string, OrganizeTreeResultModel>>();
            var all = await _organizeRepository.GetListAsync(m => m.IsDel == false);

            list = ResolveTree(all, Guid.Empty.ToString("N"));

            return ResultModel.Success(list);
        }

        private List<TreeResultModel<string, OrganizeTreeResultModel>> ResolveTree(IList<OrganizeEntity> all, string parentId)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort)
                .Select(m => new TreeResultModel<string, OrganizeTreeResultModel>
                {
                    Id = m.Id,
                    Label = m.OrganizeName,
                    Item = _mapper.Map<OrganizeTreeResultModel>(m),
                    Children = ResolveTree(all, m.Id)
                }).ToList();
        }

    }
}
