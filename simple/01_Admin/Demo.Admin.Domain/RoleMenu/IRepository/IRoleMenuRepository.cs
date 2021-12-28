 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IRoleMenuRepository : IRepository<RoleMenuEntity>
    {
        Task<IList<RoleMenuEntity>> Query(RoleMenuQueryModel model);
    }
}
 