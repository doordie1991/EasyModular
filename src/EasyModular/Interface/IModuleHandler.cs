using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EasyModular
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public interface IModuleHandler
    {
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <returns></returns>
        IList<IModuleDescriptor> LoadModules();
    }
}
