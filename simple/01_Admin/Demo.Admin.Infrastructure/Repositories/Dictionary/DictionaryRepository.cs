
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class DictionaryRepository : RepositoryBase<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<DictionaryEntity>> Query(DictionaryQueryModel model)
        {
            var conditions = await _filter.GetConditions<DictionaryEntity, DictionaryQueryModel>(model);
            var query = _dbContext.Db.Queryable<DictionaryEntity>()
                                     .LeftJoin<DictionaryGroupEntity>((x, y) => x.GroupCode == y.Code && y.IsDel == false)
                                     .Select((x, y) => new DictionaryEntity()
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
