using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 组织架构新增模型
    /// </summary>
    public class OrganizeAddModel
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        public string OrganizeCode { get; set; }

        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizeName { get; set; }

        /// <summary>
        /// 组织全称
        /// </summary>
        public string OrganizeFullName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        ///排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
