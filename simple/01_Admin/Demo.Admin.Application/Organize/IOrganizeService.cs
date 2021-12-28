 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.OrganizeService
{
    /// <summary>
    /// 组织架构服务
    /// </summary>
    public interface IOrganizeService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(OrganizeQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(OrganizeAddModel model);

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
        Task<IResultModel> Update(OrganizeUpdateModel model);

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Move(OrganizeMoveModel model);

        /// <summary>
        /// 查询组织架构树
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetTree();

    }
}
