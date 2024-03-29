
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    [SugarTable("Sys_User", "用户")]
    public partial class UserEntity : SoftDeleteEntity<string>
    {

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别（1:男,0:女）
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// 组织Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 职位等级
        /// </summary>
        public string JobLevel { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }

    }
}
