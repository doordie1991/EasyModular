 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IResourceRepository : IRepository<ResourceEntity>
    {
        Task<IList<ResourceEntity>> Query(ResourceQueryModel model);
    }
}
 