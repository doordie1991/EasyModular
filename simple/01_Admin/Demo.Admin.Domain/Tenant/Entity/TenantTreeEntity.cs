using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 租户信息(树形)
    /// </summary>
    [SugarTable("Sys_Tenant", "租户信息")]
    public class TenantTreeEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 租户编码
        /// </summary>
        public string TenantCode { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TenantTreeEntity> Child { get; set; }
    }
}
