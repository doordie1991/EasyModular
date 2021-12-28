 
using AutoMapper;
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Demo.Admin.Infrastructure;

namespace Demo.Admin.Application.TenantTypeService
{
    public class TenantTypeService : ITenantTypeService
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly ITenantTypeRepository _tenantTypeRepository;
        private readonly ITenantTypePackageRepository _tenantTypePackageRepository;
        private readonly IPackageRepository _packageRepository;
        public TenantTypeService(IMapper mapper, DbContext dbContext, ITenantTypeRepository tenantTypeRepository, ITenantTypePackageRepository tenantTypePackageRepository, IPackageRepository packageRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _tenantTypeRepository = tenantTypeRepository;
            _tenantTypePackageRepository = tenantTypePackageRepository;
            _packageRepository = packageRepository;
        }

        public async Task<IResultModel> Query(TenantTypeQueryModel model)
        {
            var result = new QueryResultModel<TenantTypeEntity>
            {
                Rows = await _tenantTypeRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(TenantTypeAddModel model)
        {
            try
            {
                if (await _tenantTypeRepository.ExistsAsync(m => m.Code == model.Code && m.IsDel == false))
                    return ResultModel.HasExists;

                _dbContext.Db.BeginTran();

                var entity = _mapper.Map<TenantTypeEntity>(model);
                await _tenantTypeRepository.InsertAsync(entity, _dbContext.Db);
                await _tenantTypePackageRepository.DeleteAsync(m => m.TenantTypeId == entity.Id, _dbContext.Db);

                var packages = new List<TenantTypePackageEntity>();
                foreach (var item in model.Packages)
                {
                    packages.Add(new TenantTypePackageEntity()
                    {
                        TenantTypeId = entity.Id,
                        PackageId = item.Id
                    });
                }
                if (packages.Any())
                    await _tenantTypePackageRepository.InsertRangeAsync(packages, _dbContext.Db);

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
            var entity = await _tenantTypeRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            try
            {
                _dbContext.Db.BeginTran();

                await _tenantTypeRepository.SoftDeleteAsync(entity, _dbContext.Db);
                await _tenantTypePackageRepository.DeleteAsync(m => m.PackageId == entity.Id, _dbContext.Db);

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
            var entity = await _tenantTypeRepository.FirstAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<TenantTypeUpdateModel>(entity);
            var packages = await _tenantTypePackageRepository.GetListAsync(m => m.TenantTypeId == entity.Id && m.IsDel == false);
            model.Packages = await _packageRepository.GetListAsync(m => packages.Select(m => m.PackageId).Contains(m.Id));

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(TenantTypeUpdateModel model)
        {
            var entity = await _tenantTypeRepository.FirstAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _tenantTypeRepository.ExistsAsync(m => m.Code == model.Code && m.Id != entity.Id && m.IsDel == false))
                return ResultModel.HasExists;

            _mapper.Map(model, entity);

            try
            {
                _dbContext.Db.BeginTran();

                await _tenantTypeRepository.UpdateAsync(entity, _dbContext.Db);
                await _tenantTypePackageRepository.DeleteAsync(m => m.TenantTypeId == entity.Id, _dbContext.Db);

                var packages = new List<TenantTypePackageEntity>();
                foreach (var item in model.Packages)
                {
                    packages.Add(new TenantTypePackageEntity()
                    {
                        TenantTypeId = entity.Id,
                        PackageId = item.Id
                    });
                }
                if (packages.Any())
                    await _tenantTypePackageRepository.InsertRangeAsync(packages, _dbContext.Db);

                _dbContext.Db.CommitTran();
            }
            catch (Exception ex)
            {
                _dbContext.Db.RollbackTran();
                return ResultModel.Failed(ex.Message);
            }

            return ResultModel.Success();
        }

        public async Task<IResultModel> Select()
        {
            var list = await _tenantTypeRepository.GetListAsync(m => m.IsDel == false);
            var result = list.OrderBy(m => m.Code).Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            });

            return ResultModel.Success(result);
        }

    }
}
