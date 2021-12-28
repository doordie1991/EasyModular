
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 媒体类型
    /// </summary>
    [SugarTable("Sys_Media_Type", "媒体类型")]
    public partial class MediaTypeEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 后缀名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

    }
}
     