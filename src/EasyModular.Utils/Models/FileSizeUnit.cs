﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 文件大小单位
    /// </summary>
    public enum FileSizeUnit
    {
        /// <summary>
        /// 字节
        /// </summary>
        [Description("B")]
        Byte,
        /// <summary>
        /// K字节
        /// </summary>
        [Description("KB")]
        K,
        /// <summary>
        /// M字节
        /// </summary>
        [Description("MB")]
        M,
        /// <summary>
        /// G字节
        /// </summary>
        [Description("GB")]
        G
    }
}
