using Demo.Admin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源新增模型
    /// </summary>
    public class ResourceAddModel
    {
        
        /// <summary>
        /// 模块
        /// </summary>
        public string Module  { get; set; }
    
        /// <summary>
        /// 资源编码
        /// </summary>
        public string Code  { get; set; }
    
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name  { get; set; }

        /// <summary>
        /// 数据来源（0:手动、1:自动）
        /// </summary>
        public DataSource Source { get; set; }

        /// <summary>
        /// 明细
        /// </summary>
        public List<ResourceDetailEntity> Details { get; set; }

        /// <summary>
        /// 过滤
        /// </summary>
        public List<ResourceFilterEntity> Filters { get; set; }

        /// <summary>
        /// 白名单
        /// </summary>
        public List<ResourceWhitelistEntity> Whitelists { get; set; }
    }
}
