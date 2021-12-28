﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        public FileInfo() { }

        /// <summary>
        /// 初始化文件信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="size">大小</param>
        public FileInfo(string name, long size = 0L)
        {
            CheckHelper.NotNull(name, nameof(name), "文件名称不能为空");

            FileName = name;
            Size = new FileSize(size);
            Ext = System.IO.Path.GetExtension(FileName)?.TrimStart('.');
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 存储名称
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public FileSize Size { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 文件的MD5值
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 完整存储路径(不包含根路径)
        /// </summary>
        public string FullPath => System.IO.Path.Combine(Path, SaveName);
    }
}
