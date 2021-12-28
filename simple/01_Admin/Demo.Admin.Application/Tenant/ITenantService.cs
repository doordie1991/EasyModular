 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.TenantService
{
    /// <summary>
    /// 租户信息服务
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(TenantQueryModel model);

        /// <summary>
        /// 查询(树形)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<TenantTreeEntity>> QueryByTree(string parentId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(TenantAddModel model);

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
        /// 信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Info();

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(TenantUpdateModel model);

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
        /// 查询树形列表
        /// </summary>
        /// <param name="tenantTypeId"></param>
        /// <returns></returns>
        Task<IResultModel> GetTree(string tenantTypeId);

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Move(TenantMoveModel model);
    }
}
