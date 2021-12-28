
using EasyModular.SqlSugar;
using EasyModular.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 权限表查询模型
    /// </summary>
    public class PermissionQueryModel : QueryPagingModel
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string PermissionCode { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string PermissionName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Like)]
        public string Action { get; set; }

        /// <summary>
        /// 请求方式（GET、POST、DELETE）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// 数据来源（0:手动、1:自动）
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public PermissionSource? Source { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public string RoleId { get; set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        [Condition(ConditionalType = ConditionalType.Equal)]
        public bool? IsAuth { get; set; }
    }
}
