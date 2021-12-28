
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户信息
    /// </summary>
    [SugarTable("Sys_Tenant", "租户信息")]
    public partial class TenantEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string DutyMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public string Trade { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 生效开始时间
        /// </summary>
        public DateTime EffectStartTime { get; set; }

        /// <summary>
        /// 生效结束时间
        /// </summary>
        public DateTime EffectEndTime { get; set; }

        /// <summary>
        /// 租户类别
        /// </summary>
        public string TenantType { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        public TenantStatus Status { get; set; }

    }
}
