
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 套餐查询模型
    /// </summary>
    public class PackageQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public PackageStatus? Status { get; set; }

    }
}
