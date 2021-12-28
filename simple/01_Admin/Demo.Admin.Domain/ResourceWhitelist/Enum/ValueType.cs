using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 值类别（0.用户、1.角色、2.组织）
    /// </summary>
    public enum ValueType
    {
        [Description("用户")]
        User = 0,
        [Description("角色")]
        Role = 1,
        [Description("组织")]
        Organize = 2
    }
}
