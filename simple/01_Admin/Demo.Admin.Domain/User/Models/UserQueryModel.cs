
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 用户表查询模型
    /// </summary>
    public class UserQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Email { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public UserStatus? Status { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeName { get; set; }


        /// <summary>
        /// 组织全称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string OrganizeFullName { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keywords { get; set; }
    }
}
