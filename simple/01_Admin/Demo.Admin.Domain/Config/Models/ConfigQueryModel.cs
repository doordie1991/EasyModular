
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 系统配置表查询模型
    /// </summary>
    public class ConfigQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 系统标题
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string SysTtile { get; set; }

        /// <summary>
        /// 系统logo
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string SysLogo { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string TenantId { get; set; }
    }
}
