 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class UserLatestSelectRepository : RepositoryBase<UserLatestSelectEntity>, IUserLatestSelectRepository
    {
        public UserLatestSelectRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<UserLatestSelectEntity>> Query(UserLatestSelectQueryModel model)
        {
            var conditions = await _filter.GetConditions<UserLatestSelectEntity, UserLatestSelectQueryModel>(model);
            var query = _dbContext.Db.Queryable<UserLatestSelectEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
