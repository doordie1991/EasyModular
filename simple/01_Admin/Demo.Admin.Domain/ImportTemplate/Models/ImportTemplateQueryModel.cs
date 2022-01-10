
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 导入模板查询模型
    /// </summary>
    public class ImportTemplateQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string TemplateName { get; set; }

    }
}
