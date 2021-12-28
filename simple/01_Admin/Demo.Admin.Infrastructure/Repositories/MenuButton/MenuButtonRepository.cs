 
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class MenuButtonRepository : RepositoryBase<MenuButtonEntity>, IMenuButtonRepository
    {
        public MenuButtonRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<MenuButtonEntity>> Query(MenuButtonQueryModel model)
        {
            var conditions = await _filter.GetConditions<MenuButtonEntity, MenuButtonQueryModel>(model);
            var query = _dbContext.Db.Queryable<MenuButtonEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
