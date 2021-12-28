 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IAuditInfoRepository : IRepository<AuditInfoEntity>
    {
        Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model);
    }
}
 