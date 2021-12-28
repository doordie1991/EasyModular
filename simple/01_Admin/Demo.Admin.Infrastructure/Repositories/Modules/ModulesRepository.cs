 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class ModulesRepository : RepositoryBase<ModulesEntity>, IModulesRepository
    {
        public ModulesRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<ModulesEntity>> Query(ModulesQueryModel model)
        {
            var conditions = await _filter.GetConditions<ModulesEntity, ModulesQueryModel>(model);
            var query = _dbContext.Db.Queryable<ModulesEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
