using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Demo.Admin.Application
{
    public class UpdatePasswordModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Required(ErrorMessage = "请输入旧密码")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "请输入新密码")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(ErrorMessage = "请输入确认密码")]
        public string ConfirmPassword { get; set; }
    }
}
