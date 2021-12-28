using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 验证码模型
    /// </summary>
    public class VerifyCodeModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
