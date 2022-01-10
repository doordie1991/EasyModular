
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class AuditInfoRepository : RepositoryBase<AuditInfoEntity>, IAuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model)
        {
            var conditions = await _filter.GetConditions<ResourceEntity, AuditInfoQueryModel>(model);
            var query = _dbContext.Db.Queryable<AuditInfoEntity>()
                                     .LeftJoin<UserEntity>((x, y) => x.Creater == y.Id && y.IsDel == false)
                                     .Select((x, y) => new AuditInfoEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         UserCode = y.UserCode,
                                         UserName = y.UserName
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
