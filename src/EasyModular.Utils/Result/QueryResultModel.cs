﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EasyModular.Utils
{
    /// <summary>
    /// 查询结果模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResultModel<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public IList<T> Rows { get; set; }

        /// <summary>
        /// 其他数据
        /// </summary>
        [JsonIgnore]
        public object Data { get; set; }
    }
}
