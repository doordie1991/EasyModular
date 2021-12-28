
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源
    /// </summary>
    [SugarTable("Sys_Resource", "资源")]
    public partial class ResourceEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 资源编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据隔离方式（0.不限、1.租户、2.行业）
        /// </summary>
        public IsolateType IsolateType { get; set; }

        /// <summary>
        /// 数据来源（0:手动、1:自动）
        /// </summary>
        public DataSource Source { get; set; }

    }
}
     