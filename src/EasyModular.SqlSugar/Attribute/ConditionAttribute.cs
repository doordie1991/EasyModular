using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 操作符特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConditionAttribute : Attribute
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public ConditionalType ConditionalType { get; set; }

        /// <summary>
        /// 类型转换
        /// </summary>
        public string ConvertType { get; set; }
    }
}
