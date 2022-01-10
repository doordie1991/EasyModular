using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 系统配置新增模型
    /// </summary>
    public class ConfigAddModel
    {
        /// <summary>
        /// 系统标题
        /// </summary>
        public string SysTtile { get; set; }

        /// <summary>
        /// 系统logo
        /// </summary>
        public string SysLogo { get; set; }

        /// <summary>
        /// 系统版权所有
        /// </summary>
        public string SysCopyright { get; set; }

    }
}
