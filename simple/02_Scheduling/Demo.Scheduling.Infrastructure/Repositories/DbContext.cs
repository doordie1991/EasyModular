 
using EasyModular.SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Infrastructure
{
    public class DbContext : DbContextBase
    {
        public DbContext(ModuleDbOption options) : base(options)
        {

        }
    }
}
 