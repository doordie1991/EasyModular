using EasyModular.SqlSugar;
using Demo.ModularB.Domain.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularB.Domain.Organization.Models;

namespace Demo.ModularB.Domain.Organization
{
    public interface IOrganizationRepository : IRepository<OrganizationEntity>
    {
        Task<IList<OrganizationEntity>> Query(OrganizationQueryModel model);
    }
}
