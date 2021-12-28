using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 菜单打开方式（0：新窗口、1：当前窗口）
    /// </summary>
    public enum MenuTarget
    {
        [Description("新窗口")]
        Blank = 0,
        [Description("当前窗口")]
        Self = 1
    }
}
