using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    public partial class LoginLogEntity
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string TenantName { get; set; }
    }
}
