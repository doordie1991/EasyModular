 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyModular;

namespace Demo.Admin.Application.RoleService
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(RoleQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(RoleAddModel model);

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
        Task<IResultModel> Update(RoleUpdateModel model);

        /// <summary>
        /// 角色树
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Tree();

        /// <summary>
        /// 菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> MenuTree(string id);

        /// <summary>
        /// 绑定菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> BindMenuPermission(RoleMenuPermissionBindModel model);

    }
}
