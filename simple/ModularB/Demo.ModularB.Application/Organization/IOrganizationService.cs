using EasyModular.Utils;
using Demo.ModularB.Domain.Organization.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.ModularB.Application.Organization.Models;

namespace Demo.ModularB.Application.Organization
{
    /// <summary>
    /// 组织服务
    /// </summary>
    public interface IOrganizationService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(OrganizationQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(OrganizationAddModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Edit(Guid id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Update(OrganizationUpdateModel model);
    }
}
