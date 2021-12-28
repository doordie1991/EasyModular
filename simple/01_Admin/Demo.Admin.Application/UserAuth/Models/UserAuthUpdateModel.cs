using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户鉴权信息更新模型
    /// </summary>
    public class UserAuthUpdateModel : UserAuthAddModel
    {
        [Required(ErrorMessage = "请选择用户鉴权信息")]
        public string Id { get; set; }
    }
}
