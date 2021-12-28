
using EasyModular.SqlSugar;
using Demo.Scheduling.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Infrastructure
{
    public class JobLogRepository : RepositoryBase<JobLogEntity>, IJobLogRepository
    {
        public JobLogRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<JobLogEntity>> Query(JobLogQueryModel model)
        {
            var conditions = await _filter.GetConditions<JobLogEntity, JobLogQueryModel>(model);

            var query = _dbContext.Db.Queryable<JobLogEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
