
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 菜单按钮表查询模型
    /// </summary>
    public class MenuButtonQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string ButtonName { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string PermissionCode { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string MenuId { get; set; }

    }
}
