using EasyModular.SqlSugar;
using Demo.ModularB.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularB.Domain.User.Models;
using System.Linq;
using SqlSugar;
using System.Linq.Expressions;

namespace Demo.ModularB.Infrastructure.Repositories.User
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {

        }

        public async Task<IList<UserEntity>> Query(UserQueryModel model)
        {
            var query = DbContext.Db.Queryable<UserEntity>().Where(m=>m.IsDel==false);

            query.WhereIF(!SqlFunc.IsNullOrEmpty(model.UserCode), m => m.UserCode.Contains(model.UserCode));
            query.WhereIF(!SqlFunc.IsNullOrEmpty(model.UserName), m => m.UserName.Contains(model.UserName));

            if (!string.IsNullOrEmpty(model.OrderFileds))
                query.OrderBy(model.OrderFileds);

            RefAsync<int> totalCount = 0;
            var data= await query.ToPageListAsync(model.PageIndex, model.PageSize, totalCount);
            model.TotalCount = totalCount;

            return data;
        }
    }
}
