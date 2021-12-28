
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Admin.Infrastructure;
using System.Linq;

namespace Demo.Admin.Application.PackageService
{
    public class PackageService : IPackageService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly IPackageRepository _packageRepository;
        private readonly IPackageRoleRepository _packageRoleRepository;

        public PackageService(IMapper mapper, DbContext dbContext, IPackageRepository packageRepository, IPackageRoleRepository packageRoleRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _packageRepository = packageRepository;
            _packageRoleRepository = packageRoleRepository;
        }

        public async Task<IResultModel> Query(PackageQueryModel model)
        {
            var result = new QueryResultModel<PackageEntity>
            {
                Rows = await _packageRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(PackageAddModel model)
        {
            try
            {
                _dbContext.Db.BeginTran();

                var entity = _mapper.Map<PackageEntity>(model);
                await _packageRepository.InsertAsync(entity, _dbContext.Db);
                await _packageRoleRepository.DeleteAsync(m => m.PackageId == entity.Id, _dbContext.Db);

                if (model.Roles.Any())
                {
                    model.Roles.ForEach(m => { m.PackageId = entity.Id; });
                    await _packageRoleRepository.InsertRangeAsync(model.Roles, _dbContext.Db);
                }

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Delete(string id)
        {
            var entity = await _packageRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            try
            {
                _dbContext.Db.BeginTran();

                await _packageRepository.SoftDeleteAsync(entity, _dbContext.Db);
                await _packageRoleRepository.DeleteAsync(m => m.PackageId == entity.Id, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Edit(string id)
        {
            var entity = await _packageRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<PackageUpdateModel>(entity);
            model.Roles = await _packageRoleRepository.QueryByPackageId(entity.Id);

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(PackageUpdateModel model)
        {
            var entity = await _packageRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            _mapper.Map(model, entity);

            try
            {
                _dbContext.Db.BeginTran();

                await _packageRepository.UpdateAsync(entity,_dbContext.Db);
                await _packageRoleRepository.DeleteAsync(m => m.PackageId == entity.Id, _dbContext.Db);

                if (model.Roles.Any())
                {
                    model.Roles.ForEach(m => { m.PackageId = entity.Id; });
                    await _packageRoleRepository.InsertRangeAsync(model.Roles, _dbContext.Db);
                }

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Enable(string id)
        {
            var entity = await _packageRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = PackageStatus.Enabled;

            var result = await _packageRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Disable(string id)
        {
            var entity = await _packageRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            entity.Status = PackageStatus.Disabled;

            var result = await _packageRepository.UpdateAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select(string tenantType)
        {
            var list = await _packageRepository.QueryByTenantType(tenantType);
            var result = list.OrderBy(m => m.Sort).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id
            });

            return ResultModel.Success(result);
        }
    }
}
