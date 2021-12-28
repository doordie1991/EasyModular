using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 媒体类型导入模型
    /// </summary>
    public class MediaTypeImportModel
    {
        /// <summary>
        /// 后缀名
        /// </summary>
        [Description("后缀名")]
        public string Ext { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Description("值")]
        public string Value { get; set; }
    }
}
