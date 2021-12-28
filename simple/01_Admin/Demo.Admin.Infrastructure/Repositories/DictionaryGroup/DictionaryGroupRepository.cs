 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class DictionaryGroupRepository : RepositoryBase<DictionaryGroupEntity>, IDictionaryGroupRepository
    {
        public DictionaryGroupRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<DictionaryGroupEntity>> Query(DictionaryGroupQueryModel model)
        {
            var conditions = await _filter.GetConditions<DictionaryGroupEntity, DictionaryGroupQueryModel>(model);
            var query = _dbContext.Db.Queryable<DictionaryGroupEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
