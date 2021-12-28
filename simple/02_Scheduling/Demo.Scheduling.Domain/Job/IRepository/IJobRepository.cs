 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Domain
{
    public interface IJobRepository : IRepository<JobEntity>
    {
        Task<IList<JobEntity>> Query(JobQueryModel model);
    }
}
 