 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Domain
{
    public interface IJobGroupRepository : IRepository<JobGroupEntity>
    {
        Task<IList<JobGroupEntity>> Query(JobGroupQueryModel model);
    }
}
 