using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Infrastructure
{
    /// <summary>
    /// 租户类别
    /// </summary>
    public static class TenantType
    {
        [Description("普通")]
        public const string Normal = "TENANTTYPE_NORMAL";

        [Description("区域")]
        public const string Area = "TENANTTYPE_AREA";

        [Description("运营商")]
        public const string Operator = "TENANTTYPE_OPERATOR";

        [Description("系统")]
        public const string System = "TENANTTYPE_SYSTEM";

        [Description("全部")]
        public const string All = "ALL";
    }
}
