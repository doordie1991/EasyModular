 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface ITenantTypeRepository : IRepository<TenantTypeEntity>
    {
        Task<IList<TenantTypeEntity>> Query(TenantTypeQueryModel model);
    }
}
 