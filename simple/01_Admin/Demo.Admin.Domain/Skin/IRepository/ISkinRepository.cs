 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface ISkinRepository : IRepository<SkinEntity>
    {
        Task<IList<SkinEntity>> Query(SkinQueryModel model);
    }
}
 