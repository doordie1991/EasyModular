 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IConfigRepository : IRepository<ConfigEntity>
    {
        Task<IList<ConfigEntity>> Query(ConfigQueryModel modeld);
    }
}
 