 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IResourceDetailRepository : IRepository<ResourceDetailEntity>
    {
        Task<IList<ResourceDetailEntity>> Query(ResourceDetailQueryModel model);
    }
}
 