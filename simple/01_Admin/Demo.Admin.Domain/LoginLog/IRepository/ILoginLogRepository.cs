 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface ILoginLogRepository : IRepository<LoginLogEntity>
    {
        Task<IList<LoginLogEntity>> Query(LoginLogQueryModel model);
    }
}
 