 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IReleaseLogRepository : IRepository<ReleaseLogEntity>
    {
        Task<IList<ReleaseLogEntity>> Query(ReleaseLogQueryModel model);
    }
}
 