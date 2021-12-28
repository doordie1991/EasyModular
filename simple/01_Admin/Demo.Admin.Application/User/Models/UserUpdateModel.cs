using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户更新模型
    /// </summary>
    public class UserUpdateModel : UserAddModel
    {
        [Required(ErrorMessage = "请选择用户")]
        public string Id { get; set; }
    }
}
