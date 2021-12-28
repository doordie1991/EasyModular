 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface ITenantRepository : IRepository<TenantEntity>
    {
        Task<IList<TenantEntity>> Query(TenantQueryModel model);
        Task<List<TenantTreeEntity>> QueryByTree(string parentId);
        Task<TenantEntity> QueryById(string id);
        Task<List<TenantEntity>> QueryByTenantType(string tenantType);
    }
}
 