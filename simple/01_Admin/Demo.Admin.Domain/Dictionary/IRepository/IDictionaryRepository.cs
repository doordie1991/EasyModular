 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IDictionaryRepository : IRepository<DictionaryEntity>
    {
        Task<IList<DictionaryEntity>> Query(DictionaryQueryModel model);
    }
}
 