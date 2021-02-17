using EasyModular.SqlSugar;
using Demo.ModularA.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularA.Domain.User.Models;

namespace Demo.ModularA.Domain.User
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IList<UserEntity>> Query(UserQueryModel model);
    }
}
