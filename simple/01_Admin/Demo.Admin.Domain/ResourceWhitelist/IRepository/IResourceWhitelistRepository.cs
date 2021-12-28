 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IResourceWhitelistRepository : IRepository<ResourceWhitelistEntity>
    {
        Task<IList<ResourceWhitelistEntity>> Query(ResourceWhitelistQueryModel model);
    }
}
 