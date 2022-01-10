using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Demo.Admin.Application
{
    /// <summary>
    /// 系统皮肤新增模型
    /// </summary>
    public class SkinAddModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 主题模式
        /// </summary>
        public string ThemeMode { get; set; }

        /// <summary>
        /// 主题颜色
        /// </summary>
        public string ThemeColor { get; set; }

        /// <summary>
        /// 字号
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// 最大窗口数量
        /// </summary>
        public int? MaxOpenCount { get; set; }

        /// <summary>
        /// 是否只保持一个子菜单的展开
        /// </summary>
        public bool? UniqueOpened { get; set; }

    }
}
