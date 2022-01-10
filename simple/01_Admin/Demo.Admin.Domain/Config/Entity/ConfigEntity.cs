
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [SugarTable("Sys_Config", "系统配置")]
    public partial class ConfigEntity : SoftDeleteEntity<string>
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
     