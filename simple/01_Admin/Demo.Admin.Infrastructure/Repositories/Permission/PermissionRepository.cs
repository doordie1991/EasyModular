
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class PermissionRepository : RepositoryBase<PermissionEntity>, IPermissionRepository
    {
        public PermissionRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<PermissionEntity>> Query(PermissionQueryModel model)
        {
            var conditions = await _filter.GetConditions<PermissionEntity, PermissionQueryModel>(model);
            var query = _dbContext.Db.Queryable<PermissionEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<IList<PermissionEntity>> QueryByUserId(string userId)
        {
            var query = _dbContext.Db.Queryable<RoleUserEntity>()
                                     .InnerJoin<RolePermissionEntity>((x, y) => x.RoleId == y.RoleId && y.IsDel == false)
                                     .InnerJoin<PermissionEntity>((x, y, z) => y.PermissionCode == z.PermissionCode && z.IsDel == false)
                                     .Where((x, y, z) => x.UserId == userId && x.IsDel == false)
                                     .Select((x, y, z) => z);

            var data = await query.ToListAsync();

            return data;
        }
    }
}
