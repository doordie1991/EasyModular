
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class RoleRepository : RepositoryBase<RoleEntity>, IRoleRepository
    {
        public RoleRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<RoleEntity>> Query(RoleQueryModel model)
        {
            var conditions = await _filter.GetConditions<RoleEntity, RoleQueryModel>(model);
            var query = _dbContext.Db.Queryable<RoleEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }


        public async Task<List<RoleEntity>> QueryByPackageId(string packageId)
        {
            var query = _dbContext.Db.Queryable<RoleEntity>()
                                     .Where(m => SqlFunc.Subqueryable<PackageRoleEntity>().Where(s => s.PackageId == packageId && s.RoleId == m.Id).Any() && m.IsDel == false);
            var data = await query.ToListAsync();

            return data;
        }
    }
}
