using EasyModular.Auth;
using EasyModular.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular.SqlSugar.Test
{
    [TestClass]
    public class DbContextTest
    {

        [TestMethod]
        public void Test_DbContext()
        {
            var dbOptions = ConfigHelper.GetModel<DbOptions>(Path.Combine(AppContext.BaseDirectory, "config/db.json"));

            //IDbContext _DbContext = new DbContext(dbOptions);

            //var db = _dbContext.Db;
        }
    }
}
