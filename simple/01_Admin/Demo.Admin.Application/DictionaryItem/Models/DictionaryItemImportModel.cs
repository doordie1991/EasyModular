using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典项导入模型
    /// </summary>
    public class DictionaryItemImportModel
    {
        /// <summary>
        /// 字典项名称
        /// </summary>
        [Description("字典项名称")]
        public string Name { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        [Description("字典项值")]
        public string Value { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Description("排序")]
        public int Sort { get; set; }
    }
}
