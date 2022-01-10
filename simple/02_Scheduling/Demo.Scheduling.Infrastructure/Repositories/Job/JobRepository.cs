 
using EasyModular.SqlSugar;
using Demo.Scheduling.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Infrastructure
{
    public class JobRepository : RepositoryBase<JobEntity>, IJobRepository
    {
        public JobRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<JobEntity>> Query(JobQueryModel model)
        {
            var conditions = await _filter.GetConditions<JobEntity, JobQueryModel>(model);

            var query = _dbContext.Db.Queryable<JobEntity>()
                                     .LeftJoin<JobGroupEntity>((x, y) => x.JobGroup == y.Code && y.IsDel == false)
                                     .Select((x, y) => new JobEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         GroupName = y.Name
                                     })
                                     .MergeTable()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
