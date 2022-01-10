using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 导入模板新增模型
    /// </summary>
    public class ImportTemplateAddModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Description("模块编码")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Description("模板编码")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string AttachmentId { get; set; }

    }
}
