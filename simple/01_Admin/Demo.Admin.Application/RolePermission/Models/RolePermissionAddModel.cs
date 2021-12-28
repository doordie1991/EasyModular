using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色权限新增模型
    /// </summary>
    public class RolePermissionAddModel
    {
            /// <summary>
            /// 角色Id
            /// </summary>
            public string RoleId { get; set; }

            /// <summary>
            /// 权限Id
            /// </summary>
            public string PermissionId { get; set; }

              }
}
