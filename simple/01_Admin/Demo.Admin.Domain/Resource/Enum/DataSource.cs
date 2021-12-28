using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 数据来源（0:手动、1:自动）
    /// </summary>
    public enum DataSource
    {
        [Description("手动")]
        Manual = 0,
        [Description("自动")]
        Auto = 1
    }
}
