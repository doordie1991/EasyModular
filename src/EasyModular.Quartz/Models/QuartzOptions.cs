using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Quartz
{
    /// <summary>
    /// Quartz配置项
    /// </summary>
    public class QuartzOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        public bool EnabledLogger { get; set; }

        /// <summary>
        /// 调度器
        /// </summary>
        public Scheduler Scheduler { get; set; }

        /// <summary>
        /// 数据存储
        /// </summary>
        public JobStore JobStore { get; set; }
    }

    public class Scheduler {
        /// <summary>
        /// 实例
        /// </summary>
        public string InstanceName { get; set; }
    }

    /// <summary>
    /// 数据存储
    /// </summary>
    public class JobStore
    {
        /// <summary>
        /// 存储方式
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 处理DB的实例
        /// </summary>
        public string DriverDelegateType { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        public string TablePrefix { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型（SqlServer/MySql/SQLite-Microsoft/OracleODP/Npgsql）
        /// </summary>
        public string Provider { get; set; }
    }
}
