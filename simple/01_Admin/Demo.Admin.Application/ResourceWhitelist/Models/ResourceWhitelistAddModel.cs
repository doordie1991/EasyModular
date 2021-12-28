using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源白名单新增模型
    /// </summary>
    public class ResourceWhitelistAddModel
    {
        
        /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId  { get; set; }
    
        /// <summary>
        /// 值
        /// </summary>
        public string Value  { get; set; }
    
        /// <summary>
        /// 值类别（0.用户、1.角色、2.组织）
        /// </summary>
        public short ValueType  { get; set; }
    }
}
