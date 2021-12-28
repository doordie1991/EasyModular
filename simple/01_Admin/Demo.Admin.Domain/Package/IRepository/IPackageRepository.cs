 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IPackageRepository : IRepository<PackageEntity>
    {
        Task<IList<PackageEntity>> Query(PackageQueryModel model);

        Task<List<PackageEntity>> QueryByTenantType(string tenantType);
    }
}
 