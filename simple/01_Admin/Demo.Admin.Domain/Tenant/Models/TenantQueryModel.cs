
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户信息表查询模型
    /// </summary>
    public class TenantQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 租户编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string TenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类别
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string TenantType { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string DutyMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Phone { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Address { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public TenantStatus? Status { get; set; }

    }
}
