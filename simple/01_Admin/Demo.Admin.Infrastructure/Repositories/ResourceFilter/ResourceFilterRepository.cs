 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class ResourceFilterRepository : RepositoryBase<ResourceFilterEntity>, IResourceFilterRepository
    {
        public ResourceFilterRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<ResourceFilterEntity>> Query(ResourceFilterQueryModel model)
        {
            var conditions = await _filter.GetConditions<ResourceFilterEntity, ResourceFilterQueryModel>(model);
            var query = _dbContext.Db.Queryable<ResourceFilterEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
