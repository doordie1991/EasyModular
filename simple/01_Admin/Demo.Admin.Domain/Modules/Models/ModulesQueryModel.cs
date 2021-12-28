
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 模块查询模型
    /// </summary>
    public class ModulesQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Code { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Name { get; set; }
             
    }
}
    