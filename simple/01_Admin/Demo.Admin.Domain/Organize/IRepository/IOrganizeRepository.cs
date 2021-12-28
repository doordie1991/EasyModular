
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IOrganizeRepository : IRepository<OrganizeEntity>
    {
        Task<IList<OrganizeEntity>> Query(OrganizeQueryModel model);
    }
}
