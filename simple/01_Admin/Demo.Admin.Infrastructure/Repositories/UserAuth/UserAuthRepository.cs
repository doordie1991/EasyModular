
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class UserAuthRepository : RepositoryBase<UserAuthEntity>, IUserAuthRepository
    {
        public UserAuthRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<UserAuthEntity>> Query(UserAuthQueryModel model)
        {
            var conditions = await _filter.GetConditions<UserAuthEntity, UserAuthQueryModel>(model);
            var query = _dbContext.Db.Queryable<UserAuthEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
