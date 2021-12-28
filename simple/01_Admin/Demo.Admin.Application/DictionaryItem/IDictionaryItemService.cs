 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.Admin.Application.DictionaryItemService
{
    /// <summary>
    /// 字典项服务
    /// </summary>
    public interface IDictionaryItemService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(DictionaryItemQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(DictionaryItemAddModel model);

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
        Task<IResultModel> Update(DictionaryItemUpdateModel model);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<IResultModel> Import(DictionaryItemAddModel model, IFormFile formFile);

    }
}
