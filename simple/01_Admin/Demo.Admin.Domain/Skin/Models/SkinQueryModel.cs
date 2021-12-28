
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 系统皮肤查询模型
    /// </summary>
    public class SkinQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string UserId { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Theme { get; set; }

    }
}
