
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色菜单表 查询模型
    /// </summary>
    public class RoleMenuQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string MenuId { get; set; }

    }
}
