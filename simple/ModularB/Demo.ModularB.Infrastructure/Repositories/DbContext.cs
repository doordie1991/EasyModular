﻿using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularB.Infrastructure.Repositories
{
    public class DbContext : DbContextBase
    {
        public DbContext(DbOptions options) : base(options)
        {

        }
    }
}
