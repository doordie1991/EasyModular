
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class LoginLogRepository : RepositoryBase<LoginLogEntity>, ILoginLogRepository
    {
        public LoginLogRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<LoginLogEntity>> Query(LoginLogQueryModel model)
        {
            var conditions = await _filter.GetConditions<LoginLogEntity, LoginLogQueryModel>(model);
            var query = _dbContext.Db.Queryable<LoginLogEntity>()
                                     .LeftJoin<TenantEntity>((x, y) => x.TenantId == y.Id && y.IsDel == false)
                                     .Select((x, y) => new LoginLogEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         TenantName = y.TenantName
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
