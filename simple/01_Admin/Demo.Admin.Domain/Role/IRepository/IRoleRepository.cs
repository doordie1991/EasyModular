 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        Task<IList<RoleEntity>> Query(RoleQueryModel model);

        Task<List<RoleEntity>> QueryByPackageId(string packageId);
    }
}
 