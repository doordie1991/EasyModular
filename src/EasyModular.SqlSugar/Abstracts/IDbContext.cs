using EasyModular.Auth;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        SqlSugarClient Db { get; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        DbOptions DbOptions { get; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        ILoginInfo LoginInfo { get; set; }

        /// <summary>
        /// 获取数据库实例
        /// </summary>
        /// <param name="dbOptions"></param>
        /// <returns></returns>
        SqlSugarClient GetInstance();
    }
}
