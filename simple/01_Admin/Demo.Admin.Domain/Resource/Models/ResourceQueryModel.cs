
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源查询模型
    /// </summary>
    public class ResourceQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 模块
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Module { get; set; }

        /// <summary>
        /// 资源编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }

        /// <summary>
        /// 数据来源（0:手动、1:自动）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public DataSource? Source { get; set; }

    }
}
