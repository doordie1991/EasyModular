﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 可选项返回模型
    /// </summary>
    public class OptionResultModel<T>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 扩展值
        /// </summary>
        public object ExtensionValue { get; set; }
    }

    /// <summary>
    /// 可选项返回模型
    /// </summary>
    public class OptionResultModel : OptionResultModel<object>
    {
    }
}