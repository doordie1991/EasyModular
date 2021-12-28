using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 用户树形模型
    /// </summary>
    public class UserTreeResultModel
    {
        /// <summary>
        /// 源编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 类型 0：部门 1：人员
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        /// 账号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string OrganizeName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { get; set; }
    }
}
