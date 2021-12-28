 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IDictionaryGroupRepository : IRepository<DictionaryGroupEntity>
    {
        Task<IList<DictionaryGroupEntity>> Query(DictionaryGroupQueryModel model);
    }
}
 