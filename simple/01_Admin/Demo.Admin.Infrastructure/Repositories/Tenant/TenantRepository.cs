
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Admin.Infrastructure
{
    public class TenantRepository : RepositoryBase<TenantEntity>, ITenantRepository
    {
        public TenantRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<TenantEntity>> Query(TenantQueryModel model)
        {
            var conditions = await _filter.GetConditions<TenantEntity, TenantQueryModel>(model);
            var query = _dbContext.Db.Queryable<TenantEntity>()
                                     .LeftJoin<TenantTypeEntity>((x, y) => x.TenantType == y.Code && y.IsDel == false)
                                     .LeftJoin<PackageEntity>((x, y, z) => x.PackageId == z.Id && z.IsDel == false)
                                     .Select((x, y, z) => new TenantEntity()
                                     {
                                         Id = x.Id.SelectAll(),

                                         TenantTypeName = y.Name,
                                         Package = z.Name
                                     })
                                     .MergeTable()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<List<TenantTreeEntity>> QueryByTree(string parentId)
        {
            var data = await _dbContext.Db.Queryable<TenantTreeEntity>()
                                          .Where(m => m.IsDel == false)
                                          .ToChildListAsync(m => m.ParentId, parentId);

            return data;
        }

        public async Task<TenantEntity> QueryById(string id)
        {
            var query = _dbContext.Db.Queryable<TenantEntity>()
                                     .LeftJoin<TenantTypeEntity>((x, y) => x.TenantType == y.Code && y.IsDel == false)
                                     .LeftJoin<PackageEntity>((x, y, z) => x.PackageId == z.Id && z.IsDel == false)
                                     .Where((x, y, z) => x.Id == id && x.IsDel == false)
                                     .Select((x, y, z) => new TenantEntity()
                                     {
                                         Id = x.Id,
                                         TenantCode = x.TenantCode,
                                         TenantName = x.TenantName,
                                         DutyMan = x.DutyMan,
                                         Phone = x.Phone,
                                         Email = x.Email,
                                         Address = x.Address,
                                         Description = x.Description,
                                         Trade = x.Trade,
                                         ParentId = x.ParentId,
                                         EffectStartTime = x.EffectStartTime,
                                         EffectEndTime = x.EffectEndTime,
                                         TenantType = x.TenantType,
                                         Status = x.Status,

                                         TenantTypeName = y.Name,
                                         Package = z.Name
                                     });


            return await query.FirstAsync();
        }

        public async Task<List<TenantEntity>> QueryByTenantType(string tenantType)
        {
            var conditions = await _filter.GetConditions<TenantEntity>();

            var query = _dbContext.Db.Queryable<TenantEntity>()
                                     .LeftJoin<TenantTypeEntity>((x, y) => x.TenantType == y.Code && y.IsDel == false)
                                     .LeftJoin<PackageEntity>((x, y, z) => x.PackageId == z.Id && z.IsDel == false)
                                     .Where(conditions)
                                     .WhereIF(!string.IsNullOrEmpty(tenantType), (x, y, z) => x.TenantType == tenantType)
                                     .Select((x, y, z) => new TenantEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         TenantTypeName = y.Name,
                                         Package = z.Name
                                     });


            return await query.ToListAsync();
        }
    }
}
