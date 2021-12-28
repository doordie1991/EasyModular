using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 套餐新增模型
    /// </summary>
    public class PackageAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name  { get; set; }
    
        /// <summary>
        /// 描述
        /// </summary>
        public string Description  { get; set; }
    
        /// <summary>
        /// 费用
        /// </summary>
        public double? Cost  { get; set; }
    
        /// <summary>
        /// 有效期
        /// </summary>
        public double? ValidityPeriod  { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态(0.创建、1.启用、-1禁用)
        /// </summary>
        public PackageStatus Status { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<PackageRoleEntity> Roles { get; set; }
    }
}
