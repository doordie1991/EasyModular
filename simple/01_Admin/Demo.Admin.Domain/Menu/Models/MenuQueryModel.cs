
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 菜单表查询模型
    /// </summary>
    public class MenuQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 父节点Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string MenuName { get; set; }

    }
}
