 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        Task<IList<PermissionEntity>> Query(PermissionQueryModel model);

        Task<IList<PermissionEntity>> QueryByUserId(string userId);
    }
}
 