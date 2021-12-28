
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典分组表查询模型
    /// </summary>
    public class UserLatestSelectQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string UserId { get; set; }

        /// <summary>
        /// 关联用户ID
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RelationId { get; set; }

    }
}
