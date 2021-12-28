using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Application
{
    public class VerifyCodeResultModel
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 验证码图片的Base64字符串
        /// </summary>
        public string Base64String { get; set; }
    }
}
