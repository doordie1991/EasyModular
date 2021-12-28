using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户类别套餐新增模型
    /// </summary>
    public class TenantTypePackageAddModel
    {
        
        /// <summary>
        /// 租户类别Id
        /// </summary>
        public string TenantTypeId  { get; set; }
    
        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId  { get; set; }
    }
}
