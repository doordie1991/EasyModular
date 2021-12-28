 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IDictionaryItemRepository : IRepository<DictionaryItemEntity>
    {
        Task<IList<DictionaryItemEntity>> Query(DictionaryItemQueryModel model);
    }
}
 