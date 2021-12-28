using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 登录日志更新模型
    /// </summary>
    public class LoginLogUpdateModel : LoginLogAddModel
    {
        [Required(ErrorMessage = "请选择登录日志")]
        public string Id { get; set; }
    }
}
