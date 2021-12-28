
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 资源白名单
    /// </summary>
    [SugarTable("Sys_Resource_Whitelist", "资源白名单")]
    public partial class ResourceWhitelistEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 值类别（0.用户、1.角色、2.组织）
        /// </summary>
        public ValueType ValueType { get; set; }

    }
}
     