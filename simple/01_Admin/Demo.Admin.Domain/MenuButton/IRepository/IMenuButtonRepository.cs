 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IMenuButtonRepository : IRepository<MenuButtonEntity>
    {
        Task<IList<MenuButtonEntity>> Query(MenuButtonQueryModel model);

    }
}
 