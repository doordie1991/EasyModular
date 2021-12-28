using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 数据隔离方式（0.不限、1.租户、2.行业）
    /// </summary>
    public enum IsolateType
    {
        [Description("不限")]
        All = 0,
        [Description("租户")]
        Tenant = 1,
        [Description("行业")]
        Trade = 2
    }
}
