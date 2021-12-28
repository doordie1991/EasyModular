
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 组织移动模型
    /// </summary>
    public class OrganizeMoveModel
    {
        /// <summary>
        /// 源菜单Id
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 目标菜单Id
        /// </summary>
        public string TargetId { get; set; }
    }
}
