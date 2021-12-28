
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IRoleUserRepository : IRepository<RoleUserEntity>
    {
        Task<IList<RoleUserEntity>> Query(RoleUserQueryModel model);

        Task<IList<RoleUserEntity>> QueryByRoleCode(string roleCode);

        Task<IList<RoleUserEntity>> QueryByUserId(string userId);
    }
}
