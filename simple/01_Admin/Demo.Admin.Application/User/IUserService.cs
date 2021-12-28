 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.UserService
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(UserQueryModel model);

        /// <summary>
        /// 根据多个账号查询
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> QueryByUserCodes(List<string> userCodes);


        /// <summary>
        /// 根据多个用户Id查询
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> QueryByUserIds(List<string> userIds);

        /// <summary>
        /// 查询最近选择
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> QueryLatestSelect(UserQueryModel model);

        /// <summary>
        /// 查询同组织
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> QueryBySameOrg(UserQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(UserAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(string id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(string id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(UserUpdateModel model);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Enable(string id);

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Disable(string id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdatePassword(UpdatePasswordModel model);

        /// <summary>
        /// 用户下拉
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select(string keywords);

        /// <summary>
        /// 保存用户最近选择
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IResultModel> SaveLatestSelect(List<string> ids);

        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetTree();

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Move(UserMoveModel model);
    }
}
