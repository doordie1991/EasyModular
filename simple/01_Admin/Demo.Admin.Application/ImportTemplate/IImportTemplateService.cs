 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Admin.Application.ImportTemplateService
{
    /// <summary>
    /// 导入模板服务
    /// </summary>
    public interface IImportTemplateService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ImportTemplateQueryModel model);

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        Task<IResultModel> QueryByCode(string moduleCode, string templateCode);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(ImportTemplateAddModel model);

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
        Task<IResultModel> Update(ImportTemplateUpdateModel model);

    }
}
