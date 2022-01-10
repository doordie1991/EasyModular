
using EasyModular.SqlSugar;
using Demo.Admin.Domain;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular.Utils;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Admin.Infrastructure
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IDbContext context, IQueryFilter filter) : base(context, filter)
        {

        }

        public async Task<IList<UserEntity>> Query(UserQueryModel model)
        {
            var conditions = await _filter.GetConditions<UserEntity, UserQueryModel>(model);
            var query = _dbContext.Db.Queryable<UserEntity>()
                                     .LeftJoin<OrganizeEntity>((x, y) => x.OrganizeId == y.Id && y.IsDel == false)
                                     .Select((x, y) => new UserEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         OrganizeFullName = y.OrganizeFullName
                                     })
                                     .MergeTable()
                                     .Where(conditions)
                                     .OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<IList<UserEntity>> QueryByUserCodes(List<string> userCodes)
        {
            var query = _dbContext.Db.Queryable<UserEntity>()
                                     .LeftJoin<OrganizeEntity>((x, y) => x.OrganizeId == y.Id && y.IsDel == false)
                                     .Where((x, y) => userCodes.Contains(x.UserCode) && x.IsDel == false)
                                     .Select((x, y) => new UserEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         OrganizeFullName = y.OrganizeFullName
                                     });

            var data = await query.ToListAsync();

            return data;
        }

        public async Task<IList<UserEntity>> QueryByUserIds(List<string> userIds)
        {
            var query = _dbContext.Db.Queryable<UserEntity>()
                                     .LeftJoin<OrganizeEntity>((x, y) => x.OrganizeId == y.Id && y.IsDel == false)
                                     .Where((x, y) => userIds.Contains(x.Id) && x.IsDel == false)
                                     .Select((x, y) => new UserEntity()
                                     {
                                         Id = x.Id.SelectAll(),
                                         OrganizeFullName = y.OrganizeFullName
                                     });

            var data = await query.ToListAsync();

            return data;
        }

        public async Task<List<UserEntity>> QueryByRoleId(string roleId)
        {
            var query = _dbContext.Db.Queryable<UserEntity>()
                                    .LeftJoin<OrganizeEntity>((x, y) => x.OrganizeId == y.Id && y.IsDel == false)
                                    .InnerJoin<RoleUserEntity>((x, y, z) => x.Id == z.UserId && z.RoleId == roleId && z.IsDel == false)
                                    .Where((x, y, z) => x.IsDel == false)
                                    .Select((x, y, z) => new UserEntity()
                                    {
                                        Id = x.Id.SelectAll(),
                                        OrganizeFullName = y.OrganizeFullName
                                    });

            var data = await query.ToListAsync();

            return data;
        }

        public async Task<IList<UserLatestSelectEntity>> QueryLatestSelect(UserQueryModel model)
        {
            var query = _dbContext.Db.Queryable<UserLatestSelectEntity, UserEntity, OrganizeEntity>((x, y, z) => new JoinQueryInfos(JoinType.Inner, x.RelationId == y.Id && y.IsDel == false, JoinType.Left, y.OrganizeId == z.Id && z.IsDel == false))
                .Where((x, y, z) => x.UserId == _dbContext.LoginInfo.UserId && x.IsDel == false);

            query.WhereIF(!string.IsNullOrEmpty(model.OrganizeName), (x, y, z) => z.OrganizeFullName.Contains(model.OrganizeName));

            query.WhereIF(!string.IsNullOrEmpty(model.UserCode), (x, y, z) => y.UserCode.Contains(model.UserCode));
            query.WhereIF(!string.IsNullOrEmpty(model.UserName), (x, y, z) => y.UserName.Contains(model.UserName));
            query.WhereIF(!string.IsNullOrEmpty(model.Keywords), (x, y, z) => y.UserCode.Contains(model.Keywords) || y.UserName.Contains(model.Keywords) || y.OrganizeFullName.Contains(model.Keywords));

            query.Select((x, y, z) => new UserLatestSelectEntity()
            {
                Id = y.Id.SelectAll(),
                OrganizeFullName = z.OrganizeFullName
            });

            query.OrderBy((x, y, z) => x.Timestamp, OrderByType.Desc);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<IList<UserEntity>> QueryBySameOrg(UserQueryModel model)
        {
            var query = _dbContext.Db.Queryable<UserEntity>()
                                     .LeftJoin<OrganizeEntity>((x, y) => x.OrganizeId == y.Id && y.IsDel == false)
                                     .Where((x, y) => x.OrganizeId == _dbContext.LoginInfo.OrganizeId && x.IsDel == false);

            query.WhereIF(!string.IsNullOrEmpty(model.OrganizeName), (x, y) => y.OrganizeFullName.Contains(model.OrganizeName));
            query.WhereIF(!string.IsNullOrEmpty(model.UserCode), (x, y) => x.UserCode.Contains(model.UserCode));
            query.WhereIF(!string.IsNullOrEmpty(model.UserName), (x, y) => x.UserName.Contains(model.UserName));
            query.WhereIF(!string.IsNullOrEmpty(model.Keywords), (x, y) => x.UserCode.Contains(model.Keywords) || x.UserName.Contains(model.Keywords) || y.OrganizeFullName.Contains(model.Keywords));

            query.Select((x, y) => new UserEntity()
            {
                Id = x.Id.SelectAll(),
                OrganizeFullName = y.OrganizeFullName
            });

            query.OrderBy((x, y) => x.CreatedTime, OrderByType.Desc);

            RefAsync<int> totalCount = 0;
            var data = await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }

        public async Task<IList<UserEntity>> GetTop(string keywords, int count)
        {
            var query = _dbContext.Db.Queryable<UserEntity>().Where(m => m.IsDel == false);
            query.WhereIF(!string.IsNullOrEmpty(keywords), m => m.UserCode.Contains(keywords) || m.UserName.Contains(keywords) || keywords.Contains(m.UserCode) || keywords.Contains(m.UserName));
            query.Take(count).OrderBy(" UserCode asc");

            var data = await query.ToListAsync();

            return data;
        }

        public async Task<string> GetUserNames(string userCodes)
        {
            var query = _dbContext.Db.Queryable<UserEntity>().Where(m => userCodes.Contains(m.UserCode) && m.IsDel == false);
            var data = await query.ToListAsync();

            return string.Join(",", data.Select(m => m.UserName).ToArray());
        }

        public async Task<bool> UpdatePassword(string id, string password)
        {
            return await _dbContext.Db.Updateable<UserEntity>()
                                  .SetColumns(m => m.Password == password)
                                  .Where(m => m.Id == id)
                                  .ExecuteCommandAsync() > 0;
        }
    }
}
