using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EasyModular.Jwt
{
    /// <summary>
    /// JWT处理接口
    /// </summary>
    public interface IJwtHandler
    {
        /// <summary>
        /// Jwt创建
        /// </summary>
        /// <param name="claims">信息</param>
        /// <param name="extendData">扩展数据</param>
        /// <returns></returns>
        JwtTokenModel Build(Claim[] claims, string extendData);
    }
}
