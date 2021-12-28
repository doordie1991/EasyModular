
using EasyModular.SqlSugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Domain
{
    /// <summary>
    /// 套餐
    /// </summary>
    [SugarTable("Sys_Package", "套餐")]
    public partial class PackageEntity : SoftDeleteEntity<string>
    {
         /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public double? Cost { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public double? ValidityPeriod { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PackageStatus Status { get; set; }

    }
}
     