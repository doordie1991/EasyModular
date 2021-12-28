
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Admin.Infrastructure;
using EasyModular;
using EasyModular.Auth;
using EasyModular.SqlSugar;
using System.Linq;

namespace Demo.Admin.Application.TenantService
{
    public class TenantService : ITenantService
    {
        private readonly IMapper _mapper;
        private readonly ILoginInfo _loginInfo;
        private readonly DbContext _dbContext;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantTypeRepository _tenantTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly IPackageRoleRepository _packageRoleRepository;
        private readonly IPasswordHandler _passwordHandler;
        public TenantService(IMapper mapper
            , DbContext dbContext
            , ILoginInfo loginInfo
            , ITenantRepository tenantRepository
            , ITenantTypeRepository tenantTypeRepository
            , IUserRepository userRepository
            , IRoleRepository roleRepository
            , IRoleUserRepository roleUserRepository
            , IPackageRoleRepository packageRoleRepository
            , IPasswordHandler passwordHandler
            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _loginInfo = loginInfo;
            _tenantRepository = tenantRepository;
            _tenantTypeRepository = tenantTypeRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleUserRepository = roleUserRepository;
            _packageRoleRepository = packageRoleRepository;
            _passwordHandler = passwordHandler;
        }

        public async Task<IResultModel> Query(TenantQueryModel model)
        {
            var result = new QueryResultModel<TenantEntity>
            {
                Rows = await _tenantRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IList<TenantTreeEntity>> QueryByTree(string parentId)
        {
            var result = await _tenantRepository.QueryByTree(parentId);
            return result;
        }

        public async Task<IResultModel> Add(TenantAddModel model)
        {
            var tenant = _mapper.Map<TenantEntity>(model);

            try
            {
                _dbContext.Db.BeginTran();
                //�⻧
                await _tenantRepository.InsertAsync(tenant, _dbContext.Db);

                //����Ա
                var admins = _mapper.Map<List<UserEntity>>(model.Admins);
                var adminRole = await _packageRoleRepository.FirstAsync(m => m.PackageId == tenant.PackageId && m.IsTop == true && m.IsDel == false);
                if (adminRole == null)
                    throw new Exception("ѡ����ײ���δά�����Ȩ�޽�ɫ");

                foreach (var admin in admins)
                {
                    if (await _userRepository.ExistsAsync(m => m.UserCode == admin.UserCode && m.IsDel == false))
                        throw new Exception($"�˺ţ�{admin.UserCode}�Ѵ���");

                    admin.Password = _passwordHandler.Encrypt(admin.UserCode, admin.Password);
                    admin.TenantId = tenant.Id;

                    await _userRepository.InsertAsync(admin, _dbContext.Db);
                    //��ɫ�û�
                    var roleUser = new RoleUserEntity()
                    {
                        RoleId = adminRole.RoleId,
                        UserId = admin.Id
                    };
                    await _roleUserRepository.InsertAsync(roleUser, _dbContext.Db);
                }

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success(tenant);
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _tenantRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var result = await _tenantRepository.SoftDeleteAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _tenantRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<TenantUpdateModel>(entity);
            var adminRole = await _packageRoleRepository.FirstAsync(m => m.PackageId == entity.PackageId && m.IsTop == true && m.IsDel == false);
            model.Admins = await _userRepository.QueryByRoleId(adminRole.RoleId);

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Info()
        {
            var entity = await _tenantRepository.FirstAsync(_loginInfo.TenantId);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<TenantUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(TenantUpdateModel model)
        {
            var tenant = await _tenantRepository.FirstAsync(model.Id);
            if (tenant == null)
                return ResultModel.NotExists;

            //�ɹ���Ա����
            var adminRole = await _packageRoleRepository.FirstAsync(m => m.PackageId == tenant.PackageId && m.IsTop == true && m.IsDel == false);
            if (adminRole == null)
                return ResultModel.Failed("ѡ����ײ���δά�����Ȩ�޽�ɫ");

            var oldAdmins = await _userRepository.QueryByRoleId(adminRole.RoleId);

            _mapper.Map(model, tenant);

            try
            {
                _dbContext.Db.BeginTran();

                //�⻧
                await _tenantRepository.UpdateAsync(tenant, _dbContext.Db);

                //�¹���Ա
                var newAdmins = _mapper.Map<List<UserEntity>>(model.Admins);

                //ɾ��=>���ɹ���Ա���ϡ����ڶ����¹���Ա���ϡ������ڵĹ���Ա
                var delAdmins = oldAdmins.Select(m => m.UserCode).Except(newAdmins.Select(m => m.UserCode));
                foreach (var item in delAdmins)
                {
                    var data = oldAdmins.First(m => m.UserCode == item);
                    await _userRepository.SoftDeleteAsync(data);
                }

                //����=>���¹���Ա���ϡ����ڶ����ɹ���Ա���ϡ������ڵĹ���Ա
                var addAdmins = newAdmins.Select(m => m.UserCode).Except(oldAdmins.Select(m => m.UserCode));
                foreach (var item in addAdmins)
                {
                    var admin = oldAdmins.First(m => m.UserCode == item);

                    if (await _userRepository.ExistsAsync(m => m.UserCode == admin.UserCode && m.IsDel == false))
                        throw new Exception($"�˺ţ�{admin.UserCode}�Ѵ���");

                    admin.Password = _passwordHandler.Encrypt(admin.UserCode, admin.Password);
                    admin.TenantId = tenant.Id;

                    await _userRepository.InsertAsync(admin, _dbContext.Db);
                    //��ɫ�û�
                    var roleUser = new RoleUserEntity()
                    {
                        RoleId = adminRole.RoleId,
                        UserId = admin.Id
                    };
                    await _roleUserRepository.InsertAsync(roleUser, _dbContext.Db);
                }

                _dbContext.Db.CommitTran();

            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success(tenant);
        }

        public async Task<IResultModel> Enable(string id)
        {
            var entity = await _tenantRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = TenantStatus.Enabled;

            var result = await _tenantRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Disable(string id)
        {
            var entity = await _tenantRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = TenantStatus.Disabled;

            var result = await _tenantRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> GetTree(string tenantType)
        {
            var list = new List<TreeResultModel<string, TenantTreeResultModel>>();
            var all = await _tenantRepository.QueryByTenantType(tenantType);
            list = ResolveTree(all, _loginInfo.TenantId);

            return ResultModel.Success(list);
        }

        private List<TreeResultModel<string, TenantTreeResultModel>> ResolveTree(List<TenantEntity> all, string parentId)
        {
            return all.Where(m => m.ParentId == parentId).OrderBy(m => m.TenantName)
                .Select(m => new TreeResultModel<string, TenantTreeResultModel>
                {
                    Id = m.Id,
                    Label = m.TenantName,
                    Item = _mapper.Map<TenantTreeResultModel>(m),
                    Children = ResolveTree(all, m.Id)
                }).ToList();
        }

        public async Task<IResultModel> Move(TenantMoveModel model)
        {
            var entity = await _tenantRepository.FirstAsync(model.SourceId);
            if (entity == null)
                return ResultModel.NotExists;

            if (model.SourceId == model.TargetId)
                return ResultModel.Failed("�����ƶ���ԭλ��");

            entity.ParentId = model.TargetId;

            var result = await _tenantRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }
    }
}
