
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 用户鉴权信息
    /// </summary>
    [SugarTable("Sys_User_Auth", "用户鉴权信息")]
    public partial class UserAuthEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

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
     