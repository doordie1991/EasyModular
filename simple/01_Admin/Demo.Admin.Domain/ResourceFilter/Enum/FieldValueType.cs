using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Domain
{

    /// <summary>
    /// 字段值类别（0.常量、1.变量）
    /// </summary>
    public enum FieldValueType
    {
        [Description("常量")]
        Constant = 0,
        [Description("变量")]
        Variable = 1
    }
}
