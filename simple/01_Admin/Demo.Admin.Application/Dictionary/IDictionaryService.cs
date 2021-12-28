 
using EasyModular.Utils;
using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application.DictionaryService
{
    /// <summary>
    /// 字典服务
    /// </summary>
    public interface IDictionaryService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(DictionaryQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(DictionaryAddModel model);

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
        Task<IResultModel> Update(DictionaryUpdateModel model);

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResultModel> Select(string group, string code);

        /// <summary>
        /// 查询字典树
        /// </summary>
        /// <param name="group"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResultModel> Tree(string group, string code);

    }
}
