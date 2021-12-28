using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Quartz
{
    /// <summary>
    /// 任务日志接口
    /// </summary>
    public interface ITaskLogger
    {
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="msg">消息</param>
        Task Info(string jobId, string msg);

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        Task Debug(string jobId, string msg);

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        Task Error(string jobId, string msg);
    }
}
