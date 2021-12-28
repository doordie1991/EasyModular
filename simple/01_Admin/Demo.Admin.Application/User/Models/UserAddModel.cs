using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户新增模型
    /// </summary>
    public class UserAddModel
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Required(ErrorMessage = "请输入名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
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
        [Required(ErrorMessage = "请选择性别")]
        public bool? Sex { get; set; }

        /// <summary>
        /// 状态（0:创建、1:启用、-1:禁用）
        /// </summary>
        public UserStatus Status { get; set; }


        /// <summary>
        /// 职位等级
        /// </summary>
        public string JobLevel { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrganizeId { get; set; }


        /// <summary>
        /// 角色集合
        /// </summary>
        public List<string> RoleIds { get; set; }

    }
}
