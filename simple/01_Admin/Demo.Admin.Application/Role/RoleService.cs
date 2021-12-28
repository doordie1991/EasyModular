
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Demo.Admin.Infrastructure;
using EasyModular;
using EasyModular.Auth;

namespace Demo.Admin.Application.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly ILoginInfo _loginInfo;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuButtonRepository _menuButtonRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly ITenantRepository _tenantRepository;
        public RoleService(IMapper mapper
            , DbContext dbContext
            , ILoginInfo loginInfo
            , IRoleRepository roleRepository
            , IMenuRepository menuRepository
            , IMenuButtonRepository menuButtonRepository
            , IPermissionRepository permissionRepository
            , IRoleMenuRepository roleMenuRepository
            , IRolePermissionRepository rolePermissionRepository
            , ITenantRepository tenantRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _loginInfo = loginInfo;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _menuButtonRepository = menuButtonRepository;
            _permissionRepository = permissionRepository;
            _roleMenuRepository = roleMenuRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<IResultModel> Query(RoleQueryModel model)
        {
            var result = new QueryResultModel<RoleEntity>
            {
                Rows = await _roleRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> QueryByPackageId()
        {
            var tenant = await _tenantRepository.FirstAsync(m => m.Id == _loginInfo.TenantId && m.IsDel == false);
            var data = await _roleRepository.QueryByPackageId(tenant.PackageId);

            return ResultModel.Success(data);
        }

        public async Task<IResultModel> Add(RoleAddModel model)
        {
            if (await _roleRepository.ExistsAsync(m => m.RoleCode == model.RoleCode && m.TenantId == model.TenantId))
                return ResultModel.HasExists;

            var entity = _mapper.Map<RoleEntity>(model);
            var result = await _roleRepository.InsertAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _roleRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _roleRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _roleRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<RoleUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(RoleUpdateModel model)
        {
            var entity = await _roleRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            var result = await _roleRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Tree()
        {
            var tenant = await _tenantRepository.FirstAsync(m => m.Id == _loginInfo.TenantId && m.IsDel == false);
            var all = await _roleRepository.QueryByPackageId(tenant.PackageId);

            var list = all.OrderBy(m => m.RoleName)
              .Select(m => new TreeResultModel<string, RoleEntity>
              {
                  Id = m.Id,
                  Label = m.RoleName,
                  Children = new List<TreeResultModel<string, RoleEntity>>()
              }).ToList(); ;

            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindMenuPermission(RoleMenuPermissionBindModel model)
        {
            if (!await _roleRepository.ExistsAsync(m => m.Id == model.RoleId))
                return ResultModel.Failed("½ÇÉ«²»´æÔÚ");

            var menus = new List<RoleMenuEntity>();
            var permissions = new List<RolePermissionEntity>();

            if (model.Menus != null && model.Menus.Any())
                menus = model.Menus.Select(m => new RoleMenuEntity { RoleId = model.RoleId, MenuId = m.MenuId, MenuChecked = m.MenuChecked }).ToList();

            foreach (var menu in model.Menus)
            {
                foreach (var button in menu.Buttons)
                {
                    if (button.Checked)
                    {
                        var per = _mapper.Map<RolePermissionEntity>(button);
                        per.RoleId = model.RoleId;
                        permissions.Add(per);
                    }
                }
            }

            try
            {
                _dbContext.Db.BeginTran();

                await _roleMenuRepository.DeleteAsync(m => m.RoleId == model.RoleId, _dbContext.Db);
                if (menus.Any())
                    await _roleMenuRepository.InsertRangeAsync(menus, _dbContext.Db);

                await _rolePermissionRepository.DeleteAsync(m => m.RoleId == model.RoleId, _dbContext.Db);
                if (permissions.Any())
                    await _rolePermissionRepository.InsertRangeAsync(permissions, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> MenuTree(string id)
        {
            var menus = await _menuRepository.GetListAsync(m => m.IsDel == false);
            var menuButtons = await _menuButtonRepository.GetListAsync(m => m.IsDel == false);
            var roleMenus = await _roleMenuRepository.GetListAsync(m => m.RoleId == id && m.MenuChecked == true && m.IsDel == false);
            var roleMenuButtons = await _rolePermissionRepository.GetListAsync(m => m.RoleId == id && m.IsDel == false);

            var all = _mapper.Map<List<RoleMenuPermissionTreeModel>>(menus);
            all.ForEach(m =>
            {
                m.Buttons = _mapper.Map<List<RoleMenuButtons>>(menuButtons.Where(b => b.MenuId == m.MenuId).OrderBy(b => b.Sort));
                m.Buttons.ForEach(t =>
                {
                    t.Checked = roleMenuButtons.Any(r => r.PermissionCode == t.PermissionCode);
                    t.Disabled = !roleMenus.Any(o => o.MenuId == t.MenuId);
                });
            });

            var result = new RoleMenuPermissionResultModel()
            {
                Menus = ResolveMenuTree(all, Guid.Empty.ToString("N")),
                RoleMenuIds = roleMenus.Select(m => m.MenuId).ToList()
            };

            return ResultModel.Success(result);
        }

        public List<TreeResultModel<string, RoleMenuPermissionTreeModel>> ResolveMenuTree(List<RoleMenuPermissionTreeModel> all, string parentId)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.Sort)
               .Select(m => new TreeResultModel<string, RoleMenuPermissionTreeModel>
               {
                   Id = m.MenuId,
                   Label = m.MenuName,
                   Item = _mapper.Map<RoleMenuPermissionTreeModel>(m),
                   Children = ResolveMenuTree(all, m.MenuId)
               }).ToList();
        }
    }
}
