
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典分组
    /// </summary>
    [SugarTable("Sys_Dictionary_Group", "系统配置")]
    public partial class DictionaryGroupEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 分组编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
     