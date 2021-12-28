using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典分组新增模型
    /// </summary>
    public class UserLatestSelectAddModel
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
