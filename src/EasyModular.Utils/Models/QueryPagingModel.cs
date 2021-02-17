using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 查询分页模型
    /// </summary>
    public class QueryPagingModel
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 排序字段(如：Id Asc)
        /// </summary>
        public string OrderFileds { get; set; }
    }

}
