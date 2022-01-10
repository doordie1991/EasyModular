
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 用户最近选择
    /// </summary>
    [SugarTable("Sys_User_Latest_Select", "用户最近选择")]
    public partial class UserLatestSelectEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 关联用户ID
        /// </summary>
        public string RelationId { get; set; }

        /// <summary>
        /// 最近选择时间戳
        /// </summary>
        public long Timestamp { get; set; }

    }
}
