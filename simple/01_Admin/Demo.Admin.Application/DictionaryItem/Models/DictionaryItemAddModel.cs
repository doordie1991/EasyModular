using EasyModular.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 字典项新增模型
    /// </summary>
    public class DictionaryItemAddModel
    {
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典项值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 数据类别
        /// </summary>
        public DataCategory DataCategory { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }

    }
}
