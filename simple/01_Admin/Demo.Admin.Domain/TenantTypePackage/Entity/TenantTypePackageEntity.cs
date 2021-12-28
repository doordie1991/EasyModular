
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户类别套餐
    /// </summary>
    [SugarTable("Sys_Tenant_Type_Package", "租户类别套餐")]
    public partial class TenantTypePackageEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 租户类别Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId { get; set; }

    }
}
     