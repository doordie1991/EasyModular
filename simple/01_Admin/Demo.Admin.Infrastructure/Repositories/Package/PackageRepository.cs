
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class PackageRepository : RepositoryBase<PackageEntity>, IPackageRepository
    {
        public PackageRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<PackageEntity>> Query(PackageQueryModel model)
        {
            var conditions = await _filter.GetConditions<PackageEntity, PackageQueryModel>(model);
            var query = _dbContext.Db.Queryable<PackageEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<List<PackageEntity>> QueryByTenantType(string tenantType)
        {
            var query = _dbContext.Db.Queryable<PackageEntity>()
                                     .InnerJoin<TenantTypePackageEntity>((x,y)=>x.Id==y.PackageId&&y.IsDel==false)
                                     .InnerJoin<TenantTypeEntity>((x, y,z) => y.TenantTypeId == z.Id &&z.Code==tenantType&& z.IsDel == false)
                                     .Where((x, y, z) => x.IsDel == false);

            var data = await query.ToListAsync();
            return data;
        }
    }
}
