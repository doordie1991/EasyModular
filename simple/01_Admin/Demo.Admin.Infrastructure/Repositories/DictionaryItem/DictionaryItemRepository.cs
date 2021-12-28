
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
    public class DictionaryItemRepository : RepositoryBase<DictionaryItemEntity>, IDictionaryItemRepository
    {
        public DictionaryItemRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<DictionaryItemEntity>> Query(DictionaryItemQueryModel model)
        {
            var conditions = await _filter.GetConditions<DictionaryItemEntity, DictionaryItemQueryModel>(model);
            var query = _dbContext.Db.Queryable<DictionaryItemEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
