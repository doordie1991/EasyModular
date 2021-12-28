 
using EasyModular.Utils;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Application.JobGroupService
{
    /// <summary>
    /// 任务分组服务
    /// </summary>
    public interface IJobGroupService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(JobGroupQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(JobGroupAddModel model);

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
        Task<IResultModel> Update(JobGroupUpdateModel model);

        /// <summary>
        /// 下拉
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select();

    }
}
