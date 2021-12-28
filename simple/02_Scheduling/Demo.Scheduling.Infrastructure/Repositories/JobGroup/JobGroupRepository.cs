 
using EasyModular.SqlSugar;
using Demo.Scheduling.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Infrastructure
{
    public class JobGroupRepository : RepositoryBase<JobGroupEntity>, IJobGroupRepository
    {
        public JobGroupRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<JobGroupEntity>> Query(JobGroupQueryModel model)
        {
            var conditions = await _filter.GetConditions<JobGroupEntity, JobGroupQueryModel>(model);

            var query = _dbContext.Db.Queryable<JobGroupEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
