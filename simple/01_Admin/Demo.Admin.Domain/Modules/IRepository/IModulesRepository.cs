 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IModulesRepository : IRepository<ModulesEntity>
    {
        Task<IList<ModulesEntity>> Query(ModulesQueryModel model);
    }
}
 