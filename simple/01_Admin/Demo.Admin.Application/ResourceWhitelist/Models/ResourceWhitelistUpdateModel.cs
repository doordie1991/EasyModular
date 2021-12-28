using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源白名单更新模型
    /// </summary>
    public class ResourceWhitelistUpdateModel : ResourceWhitelistAddModel
    {
        [Required(ErrorMessage = "请选择资源白名单")]
        public string Id { get; set; }
    }
}
