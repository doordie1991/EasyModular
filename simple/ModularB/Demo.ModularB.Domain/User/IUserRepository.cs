using EasyModular.SqlSugar;
using Demo.ModularB.Domain.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularB.Domain.User.Models;

namespace Demo.ModularB.Domain.User
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IList<UserEntity>> Query(UserQueryModel model);
    }
}
