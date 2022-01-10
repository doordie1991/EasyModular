
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 附件查询模型
    /// </summary>
    public class AttachmentQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 数据行Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string Id { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string FileName { get; set; }

        /// <summary>
        /// 存储名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string SaveName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string Ext { get; set; }
    }
}
