 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IUserLatestSelectRepository : IRepository<UserLatestSelectEntity>
    {
        Task<IList<UserLatestSelectEntity>> Query(UserLatestSelectQueryModel model);
    }
}
 