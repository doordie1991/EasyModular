using EasyModular.Auth;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyModular.SqlSugar
{
    /// <summary>
    /// 数据库上下文基类
    /// </summary>
    public class DbContextBase : IDbContext
    {
        public SqlSugarClient Db { get; set; }

        public DbOptions DbOptions { get; set; }

        public ILoginInfo LoginInfo { get; set; }

        public DbContextBase(DbOptions opt)
        {
            DbOptions = opt;
            Db = GetInstance();
        }

        public SqlSugarClient GetInstance()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = DbOptions.ConnectionString,//连接符字串
                DbType = DbOptions.DbType,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            if (DbOptions.IsPintLog)
            {    //添加Sql打印事件，开发中可以删掉这个代码
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine("[执行脚本]:" + sql + "\r\n" + "[执行参数]:" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                    Console.WriteLine();
                };

            }

            return db;
        }
    }
}
