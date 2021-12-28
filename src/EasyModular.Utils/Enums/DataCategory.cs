using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Utils
{
    /// <summary>
    /// 数据分类
    /// </summary>
    public enum DataCategory
    {
        [Description("企业私用")]
        Private = 0,
        [Description("行业通用")]
        General = 1
    }
}
