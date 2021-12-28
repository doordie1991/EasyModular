 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IMediaTypeRepository : IRepository<MediaTypeEntity>
    {
        Task<IList<MediaTypeEntity>> Query(MediaTypeQueryModel model);
    }
}
 