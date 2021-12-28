using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 模块数据库配置项
    /// </summary>
    public class ModuleDbOption
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 是否输出日志
        /// </summary>
        public bool IsPintLog { get; set; }

        /// <summary>
        /// 用户标识：用户Id:UserId或用户账号:UserCode
        /// </summary>
        public string UserKey { get; set; }

    }
}
