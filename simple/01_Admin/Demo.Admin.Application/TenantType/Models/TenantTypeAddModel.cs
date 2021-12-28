using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 租户类别新增模型
    /// </summary>
    public class TenantTypeAddModel
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string Code  { get; set; }
    
        /// <summary>
        /// 名称
        /// </summary>
        public string Name  { get; set; }

        /// <summary>
        /// 套餐
        /// </summary>
        public List<PackageEntity> Packages { get; set; }
    }
}
