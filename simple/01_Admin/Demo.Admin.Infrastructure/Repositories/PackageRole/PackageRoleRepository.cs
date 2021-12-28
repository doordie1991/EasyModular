 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class PackageRoleRepository : RepositoryBase<PackageRoleEntity>, IPackageRoleRepository
    {
        public PackageRoleRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<PackageRoleEntity>> Query(PackageRoleQueryModel model)
        {
            var conditions = await _filter.GetConditions<PackageRoleEntity, PackageRoleQueryModel>(model);
            var query = _dbContext.Db.Queryable<PackageRoleEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<List<PackageRoleEntity>> QueryByPackageId(string packageId)
        {
            var query = _dbContext.Db.Queryable<PackageRoleEntity>()
                                     .InnerJoin<RoleEntity>((x, y) => x.RoleId == y.Id && y.IsDel == false)
                                     .Select((x, y) => new PackageRoleEntity()
                                     {
                                         PackageId = x.PackageId,
                                         RoleId = x.RoleId,
                                         IsTop = x.IsTop,
                                         RoleName = y.RoleName,
                                         RoleCode = y.RoleCode,
                                         Remark = y.Remark
                                     })
                                     .Where(x => x.PackageId == packageId && x.IsDel == false)
                                     .OrderBy("y.RoleName asc");

            var data = await query.ToListAsync();

            return data;
        }
    }
}
