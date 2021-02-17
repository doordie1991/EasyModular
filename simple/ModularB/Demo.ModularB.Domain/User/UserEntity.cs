using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularB.Domain.User
{
    [SugarTable("Sys_User")]
    public partial class UserEntity : EntityBase
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
        /// 性别（1:男,0:女）
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否锁定，锁定后不允许在账户管理中修改
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizeName { get; set; }

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
