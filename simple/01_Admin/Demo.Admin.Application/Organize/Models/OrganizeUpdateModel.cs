using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 组织架构更新模型
    /// </summary>
    public class OrganizeUpdateModel : OrganizeAddModel
    {
        [Required(ErrorMessage = "请选择组织架构")]
        public string Id { get; set; }
    }
}
