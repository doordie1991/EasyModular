 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.PermissionService
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(PermissionQueryModel model);

        /// <summary>
        /// 根据用户Id查询
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByUserId(string userId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(PermissionAddModel model);

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
        Task<IResultModel> Update(PermissionUpdateModel model);

        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Sync();

    }
}
