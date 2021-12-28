 
using EasyModular.Utils;
using Demo.Scheduling.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Scheduling.Application.JobService
{
    /// <summary>
    /// 任务服务
    /// </summary>
    public interface IJobService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(JobQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Add(JobAddModel model);

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
        Task<IResultModel> Update(JobUpdateModel model);

        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Pause(string id);

        /// <summary>
        /// 恢复
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Resume(string id);

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Stop(string id);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> UpdateStatus(string group, string code,JobStatus status);
    }
}
