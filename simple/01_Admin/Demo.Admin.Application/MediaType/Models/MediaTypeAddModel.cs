using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 媒体类型新增模型
    /// </summary>
    public class MediaTypeAddModel
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
