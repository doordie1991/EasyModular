
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户类别
    /// </summary>
    [SugarTable("Sys_Tenant_Type", "租户类别")]
    public partial class TenantTypeEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
     