
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 字典项
    /// </summary>
    [SugarTable("Sys_Dictionary_Item", "系统配置")]
    public partial class DictionaryItemEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }
    }
}
