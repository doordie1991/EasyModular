using EasyModular.SqlSugar;
using Demo.ModularA.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularA.Domain.Organization.Models;

namespace Demo.ModularA.Domain.Organization
{
    public interface IOrganizationRepository : IRepository<OrganizationEntity>
    {
        Task<IList<OrganizationEntity>> Query(OrganizationQueryModel model);
    }
}
