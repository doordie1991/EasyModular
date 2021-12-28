 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Domain
{
    public interface IJobLogRepository : IRepository<JobLogEntity>
    {
        Task<IList<JobLogEntity>> Query(JobLogQueryModel model);
    }
}
 