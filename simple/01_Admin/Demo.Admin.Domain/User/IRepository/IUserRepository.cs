 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<IList<UserEntity>> Query(UserQueryModel model);

        Task<IList<UserEntity>> QueryByUserCodes(List<string> userCodes);

        Task<IList<UserEntity>> QueryByUserIds(List<string> userIds);

        Task<IList<UserLatestSelectEntity>> QueryLatestSelect(UserQueryModel model);

        Task<IList<UserEntity>> QueryBySameOrg(UserQueryModel model);

        Task<List<UserEntity>> QueryByRoleId(string roleId);

        Task<bool> UpdatePassword(string id, string password);

        Task<IList<UserEntity>> GetTop(string keywords, int count);

        Task<string> GetUserNames(string userCodes);

    }
}
 