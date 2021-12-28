
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IMenuRepository : IRepository<MenuEntity>
    {
        Task<IList<MenuEntity>> Query(MenuQueryModel model);

        Task<IList<MenuEntity>> QueryByUserId(string userId);
    }
}
