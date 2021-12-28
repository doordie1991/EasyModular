using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    public partial class AuditInfoEntity
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string TenantName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string UserName { get; set; }
    }
}
