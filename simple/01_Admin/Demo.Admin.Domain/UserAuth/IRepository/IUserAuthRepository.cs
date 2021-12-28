 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IUserAuthRepository : IRepository<UserAuthEntity>
    {
        Task<IList<UserAuthEntity>> Query(UserAuthQueryModel model);
    }
}
 