using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EasyModular.Auth
{
    /// <summary>
    /// 权限验证处理接口
    /// </summary>
    public interface IPermissionValidateHandler
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        bool Validate(IDictionary<string, string> routeValues, HttpMethod httpMethod);
    }
}
