
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    public class RoleUserRepository : RepositoryBase<RoleUserEntity>, IRoleUserRepository
    {
        public RoleUserRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<RoleUserEntity>> Query(RoleUserQueryModel model)
        {
            var conditions = await _filter.GetConditions<RoleUserEntity, RoleUserQueryModel>(model);
            var query = _dbContext.Db.Queryable<RoleUserEntity>()
                                     .InnerJoin<UserEntity>((x, y) => x.UserId == y.Id && y.IsDel == false)
                                     .Select((x, y) => new RoleUserEntity
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

        public async Task<IList<RoleUserEntity>> QueryByRoleCode(string roleCode)
        {
            var query = _dbContext.Db.Queryable<RoleUserEntity>()
                                     .InnerJoin<RoleEntity>((x, y) => x.RoleId == y.Id && y.RoleCode == roleCode && y.IsDel == false)
                                     .InnerJoin<UserEntity>((x, y, z) => x.UserId == z.Id && z.IsDel == false)
                                     .Select((x, y, z) => new RoleUserEntity
                                     {
                                         Id = x.Id,
                                         UserId = x.UserId,
                                         UserCode = z.UserCode,
                                         UserName = z.UserName,
                                     });


            var data = await query.ToListAsync();
            return data;
        }

        public async Task<IList<RoleUserEntity>> QueryByUserId(string userId)
        {
            var query = _dbContext.Db.Queryable<RoleUserEntity>()
                                     .InnerJoin<RoleEntity>((x, y) => x.RoleId == y.Id && y.IsDel == false)
                                     .Where((x, y) => x.UserId == userId && x.IsDel == false)
                                     .Select((x, y) => new RoleUserEntity
                                     {
                                         RoleCode = y.RoleCode,
                                         RoleName = y.RoleName,
                                     });

            var data = await query.ToListAsync();
            return data;
        }
    }
}
