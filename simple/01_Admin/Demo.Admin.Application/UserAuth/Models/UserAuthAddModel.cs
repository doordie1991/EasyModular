using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户鉴权信息新增模型
    /// </summary>
    public class UserAuthAddModel
    {
            /// <summary>
            /// 用户编码
            /// </summary>
            public string UserCode { get; set; }

            /// <summary>
            /// 刷新令牌
            /// </summary>
            public string RefreshToken { get; set; }

            /// <summary>
            /// 刷新令牌过期时间
            /// </summary>
            public DateTime ExpiredTime { get; set; }

            /// <summary>
            /// 最后登录时间戳
            /// </summary>
            public long LoginTime { get; set; }

            /// <summary>
            /// 最后登录IP
            /// </summary>
            public string LoginIP { get; set; }

              }
}
