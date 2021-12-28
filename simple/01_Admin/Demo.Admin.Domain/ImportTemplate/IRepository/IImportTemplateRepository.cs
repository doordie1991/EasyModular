 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IImportTemplateRepository : IRepository<ImportTemplateEntity>
    {
        Task<IList<ImportTemplateEntity>> Query(ImportTemplateQueryModel model);
    }
}
 