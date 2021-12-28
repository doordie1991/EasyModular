
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    [SugarTable("Sys_Permission", "权限")]
    public partial class PermissionEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 请求方式（GET、POST、DELETE）
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 数据来源（0:手动、1:自动）
        /// </summary>
        public PermissionSource Source { get; set; }

    }
}
