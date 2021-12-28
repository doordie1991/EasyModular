
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 导入模板
    /// </summary>
    [SugarTable("Sys_Import_Template", "导入模板")]
    public partial class ImportTemplateEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
     