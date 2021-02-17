using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ModularA.Application.Test
{
    /// <summary>
    /// 测试服务
    /// </summary>
    public interface ITestService
    {
        /// 测试事务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> TestTran();
    }
}
