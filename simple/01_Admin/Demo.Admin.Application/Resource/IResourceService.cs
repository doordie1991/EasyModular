 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.ResourceService
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ResourceQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ResourceAddModel model);

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
        Task<IResultModel> Update(ResourceUpdateModel model);

        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Permissions(ResourceUpdateModel model);


        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Sync();

    }
}
