using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 套餐角色新增模型
    /// </summary>
    public class PackageRoleAddModel
    {
        
        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId  { get; set; }
    
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId  { get; set; }

        /// <summary>
        /// 是否最高权限角色
        /// </summary>
        public bool? IsTop { get; set; }
    }
}
