using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 模块新增模型
    /// </summary>
    public class ModulesAddModel
    {
        
        /// <summary>
        /// 模块编码
        /// </summary>
        public string Code  { get; set; }
    
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name  { get; set; }
    
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon  { get; set; }
    
        /// <summary>
        /// 版本
        /// </summary>
        public string Version  { get; set; }
    
        /// <summary>
        /// 描述
        /// </summary>
        public string Description  { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}
