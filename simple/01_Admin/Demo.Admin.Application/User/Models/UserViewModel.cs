using Demo.Admin.Domain;
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application
{
    public class UserViewModel: BaseEntity<string>
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

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
