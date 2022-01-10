
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 审计信息
    /// </summary>
    [SugarTable("Sys_AuditInfo", "审计信息")]
    public partial class AuditInfoEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 控制器描述
        /// </summary>
        public string ControllerDesc { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string ActionDesc { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 结果返回
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public long ExecutionDuration { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

    }
}
     