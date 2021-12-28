
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 附件
    /// </summary>
    [SugarTable("Sys_Attachment", "附件")]
    public partial class AttachmentEntity : SoftDeleteEntity<string>
    {
        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 存储名称
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 大小(字节)
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件大小中文名
        /// </summary>
        public string SizeCn { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 是否需要授权访问
        /// </summary>
        public bool IsAuth { get; set; }

        /// <summary>
        /// 媒体类型
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
     