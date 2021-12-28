 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IPackageRoleRepository : IRepository<PackageRoleEntity>
    {
        Task<IList<PackageRoleEntity>> Query(PackageRoleQueryModel model);
        Task<List<PackageRoleEntity>> QueryByPackageId(string packageId);
    }
}
 