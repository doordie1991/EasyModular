
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典
    /// </summary>
    [SugarTable("Sys_Dictionary", "系统配置")]
    public partial class DictionaryEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
