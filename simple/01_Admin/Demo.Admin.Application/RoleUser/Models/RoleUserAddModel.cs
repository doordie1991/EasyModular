using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 角色用户新增模型
    /// </summary>
    public class RoleUserAddModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "请选择角色")]
        public string RoleId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public List<string> UserIds { get; set; }

    }
}
