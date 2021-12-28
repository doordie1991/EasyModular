 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class RoleMenuRepository : RepositoryBase<RoleMenuEntity>, IRoleMenuRepository
    {
        public RoleMenuRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<RoleMenuEntity>> Query(RoleMenuQueryModel model)
        {
            var conditions = await _filter.GetConditions<RoleMenuEntity, RoleMenuQueryModel>(model);
            var query = _dbContext.Db.Queryable<RoleMenuEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
