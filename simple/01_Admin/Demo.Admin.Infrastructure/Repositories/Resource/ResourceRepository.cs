 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Utils;

namespace Demo.Admin.Infrastructure
{
    public class ResourceRepository : RepositoryBase<ResourceEntity>, IResourceRepository
    {
        public ResourceRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<ResourceEntity>> Query(ResourceQueryModel model)
        {
            var conditions = await _filter.GetConditions<ResourceEntity, ResourceQueryModel>(model);
            var query = _dbContext.Db.Queryable<ResourceEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
