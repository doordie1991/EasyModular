
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 角色用户表查询模型
    /// </summary>
    public class RoleUserQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RoleId { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string UserName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Organize { get; set; }

    }
}
