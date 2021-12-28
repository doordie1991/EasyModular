using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.Admin.Application
{
    public class UserExportModel
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [Description("用户编码")]
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Description("用户名称")]
        public string UserName { get; set; }
    }
}
