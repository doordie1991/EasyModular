using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.ModularB.Application.Organization.Models
{
    /// <summary>
    /// 用户新增模型
    /// </summary>
    public class OrganizationAddModel
    {

        /// <summary>
        /// 组织编码
        /// </summary>
        [Required(ErrorMessage = "请输入组织编码")]
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        [Required(ErrorMessage = "请输入组织名称")]
        public string OrganizeName { get; set; }

        /// <summary>
        /// 父级编码
        /// </summary>
        [Required(ErrorMessage = "请输入父级编码")]
        public string ParentCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
