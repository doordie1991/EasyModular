
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户类别套餐查询模型
    /// </summary>
    public class TenantTypePackageQueryModel : QueryPagingModel
    {

        /// <summary>
        /// 租户类别Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string PackageId { get; set; }
    }
}
    