using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 资源明细新增模型
    /// </summary>
    public class ResourceDetailAddModel
    {
        
        /// <summary>
        /// 资源Id
        /// </summary>
        public string ResourceId  { get; set; }
    
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName  { get; set; }
    
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType  { get; set; }
    }
}
