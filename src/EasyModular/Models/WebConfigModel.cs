using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular
{
    /// <summary>
    /// web配置模型
    /// </summary>
    public class WebConfigModel
    {
        /// <summary>
        /// 是否开启审计
        /// </summary>
        public bool IsAudit { get; set; }

        /// <summary>
        /// 是否开启权限认证
        /// </summary>
        public bool IsValidate { get; set; }

        /// <summary>
        /// 是否启用单账户登录
        /// </summary>
        public bool IsSingleAccount { get; set; }

        /// <summary>
        /// 刷新token有效时间(天)，默认7天
        /// </summary>
        public int RefreshTokenExpiredTime { get; set; } = 7;

        /// <summary>
        /// Jwt
        /// </summary>
        public JwtOptions Jwt { get; set; }

        /// <summary>
        /// 缓存
        /// </summary>
        public CacheOptions Cache { get; set; }

        private string _uploadPath;
        /// <summary>
        /// 文件上传存储根路径
        /// </summary>
        public string UploadPath
        {
            get
            {
                if (string.IsNullOrEmpty(_uploadPath))
                    _uploadPath = Path.Combine(AppContext.BaseDirectory, "upload");

                if (!Path.IsPathRooted(_uploadPath))
                {
                    _uploadPath = Path.Combine(AppContext.BaseDirectory, _uploadPath);
                }

                return _uploadPath;
            }
            set => _uploadPath = value;
        }

        private string _tempPath;
        /// <summary>
        /// 临时文件存储根路径
        /// </summary>
        public string TempPath
        {
            get
            {
                if (string.IsNullOrEmpty(_tempPath))
                    _tempPath = Path.Combine(AppContext.BaseDirectory, "temp");

                if (!Path.IsPathRooted(_tempPath))
                {
                    _tempPath = Path.Combine(AppContext.BaseDirectory, _tempPath);
                }

                return _tempPath;
            }
            set => _tempPath = value;
        }
    }

    /// <summary>
    /// JWT配置项
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 消费者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 有效期(分钟，默认120)
        /// </summary>
        public int Expires { get; set; } = 120;
    }


    /// <summary>
    /// JWT配置项
    /// </summary>
    public class CacheOptions
    {
        /// <summary>
        /// 缓存模式：0、MemoryCache 1、Redis
        /// </summary>
        public CacheMode Mode { get; set; }

        /// <summary>
        /// 缓存前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
