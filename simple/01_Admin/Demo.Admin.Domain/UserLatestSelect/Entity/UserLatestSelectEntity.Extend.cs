using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    public partial class UserLatestSelectEntity
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Email { get; set; }

        /// <summary>
        /// 性别（1:男,0:女）
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool? Sex { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public UserStatus Status { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string JobName { get; set; }

        /// <summary>
        /// 组织全称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrganizeFullName { get; set; }
    }
}
