using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 菜单类型（0：节点、1：路由、2：链接）
    /// </summary>
    public enum MenuType
    {
        [Description("节点")]
        Node = 0,
        [Description("路由")]
        Route = 1,
        [Description("链接")]
        Link = 2
    }
}
