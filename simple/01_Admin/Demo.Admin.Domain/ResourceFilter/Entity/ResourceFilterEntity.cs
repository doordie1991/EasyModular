
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源过滤
    /// </summary>
    [SugarTable("Sys_Resource_Filter", "资源过滤")]
    public partial class ResourceFilterEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public ConditionalType ConditionalType { get; set; }

        /// <summary>
        /// 字段值类别（0.常量、1.变量）
        /// </summary>
        public FieldValueType FieldValueType { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldValue { get; set; }

    }
}
     