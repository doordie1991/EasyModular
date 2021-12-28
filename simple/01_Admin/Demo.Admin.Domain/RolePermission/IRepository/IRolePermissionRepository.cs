 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IRolePermissionRepository : IRepository<RolePermissionEntity>
    {
        Task<IList<RolePermissionEntity>> Query(RolePermissionQueryModel model);
    }
}
 