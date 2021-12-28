 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.PackageService
{
    /// <summary>
    /// 套餐服务
    /// </summary>
    public interface IPackageService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(PackageQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(PackageAddModel model);

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
        Task<IResultModel> Update(PackageUpdateModel model);

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
        /// 下拉
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select(string tenantType);

    }
}
