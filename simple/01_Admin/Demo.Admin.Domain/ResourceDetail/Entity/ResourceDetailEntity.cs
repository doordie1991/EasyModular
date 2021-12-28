
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源明细
    /// </summary>
    [SugarTable("Sys_Resource_Detail", "资源明细")]
    public partial class ResourceDetailEntity : SoftDeleteEntity<string>
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
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }

    }
}
     