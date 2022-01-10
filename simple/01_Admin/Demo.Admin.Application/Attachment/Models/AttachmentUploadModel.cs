using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 附件上传模型
    /// </summary>
    public class AttachmentUploadModel
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 所属模块
        /// </summary>
        [Required(ErrorMessage = "所属模块不能为空")]
        public string Module { get; set; }

        /// <summary>
        /// 所属分组
        /// </summary>
        [Required(ErrorMessage = "所属分组不能为空")]
        public string Group { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
    }
}
