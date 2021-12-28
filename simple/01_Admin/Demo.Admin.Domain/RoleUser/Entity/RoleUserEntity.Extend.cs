using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    public partial class RoleUserEntity
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Organize { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string RoleName { get; set; }
    }
}
