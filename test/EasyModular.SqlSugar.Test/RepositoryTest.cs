
using EasyModular.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular.SqlSugar.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Test_Transaction()
        {
            //var dbOptions = ConfigHelper.GetModel<DbOptions>(Path.Combine(AppContext.BaseDirectory, "config/db.json"));
            //IDbContext _DbContext = new DbContext(dbOptions);
            //var _db = _dbContext.Db;

            //var _userRepository = new UserRepository(_DbContext);
            //var _orgRepository = new OrganizationrRepository(_DbContext);

            //var userEntity = new UserEntity()
            //{
            //    UserCode = "11",
            //    UserName = "22",
            //    Password = "33"
            //};

            //var res1 = _userRepository.Insert(userEntity, _db);

            //var organizationEntity = new OrganizationEntity()
            //{
            //    OrganizeCode = "33",
            //    OrganizeName = "44"
            //};

            //var res2 = _orgRepository.Insert(organizationEntity, _db);

            //if (res1 && res2)
            //    _db.CommitTran();
        }
    }
}
