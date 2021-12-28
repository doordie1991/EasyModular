 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface ITenantTypePackageRepository : IRepository<TenantTypePackageEntity>
    {
        Task<IList<TenantTypePackageEntity>> Query(TenantTypePackageQueryModel model);
    }
}
 