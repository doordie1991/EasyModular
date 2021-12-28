
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class MenuRepository : RepositoryBase<MenuEntity>, IMenuRepository
    {
        public MenuRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<MenuEntity>> Query(MenuQueryModel model)
        {
            var conditions = await _filter.GetConditions<MenuEntity, MenuQueryModel>(model);
            var query = _dbContext.Db.Queryable<MenuEntity>()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<IList<MenuEntity>> QueryByUserId(string userId)
        {
            var query = _dbContext.Db.Queryable<RoleUserEntity>()
                                     .InnerJoin<RoleMenuEntity>((x, y) => x.RoleId == y.RoleId && y.IsDel == false)
                                     .InnerJoin<MenuEntity>((x, y, z) => y.MenuId == z.Id && z.IsDel == false)
                                     .Where((x, y, z) => x.UserId == userId && x.IsDel == false)
                                     .Select((x, y, z) => z);

            var data = await query.ToListAsync();
            return data;
        }
    }
}
