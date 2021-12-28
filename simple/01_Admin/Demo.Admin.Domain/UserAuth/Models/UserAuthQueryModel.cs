
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 用户鉴权信息表查询模型
    /// </summary>
    public class UserAuthQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string UserId { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string LoginIP { get; set; }
    }
}
