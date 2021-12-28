using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular
{
    /// <summary>
    /// 应用关闭处理接口
    /// </summary>
    public interface IAppShutdownHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <returns></returns>
        Task Handle();
    }
}
