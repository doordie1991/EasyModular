using EasyModular.Auth;
using EasyModular.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EasyModular.SqlSugar
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddEasyModularSqlSugar(this IServiceCollection services)
        {
            var dbOptions = ConfigHelper.GetModel<DbOptions>(Path.Combine(AppContext.BaseDirectory, "config/db.json"));

            if (dbOptions == null)
                return services;

            services.AddSingleton(dbOptions);

            var sp = services.BuildServiceProvider();
            var modules = sp.GetService<IList<IModuleDescriptor>>();
            var loginInfo = sp.GetService<ILoginInfo>();
            IQueryFilter filter = null;

            foreach (var module in modules)
            {

                if (module.AssemblyDescriptor.Domain == null)
                    continue;

                var moduleDbOpt = dbOptions.Modules.Where(m => m.ModuleId == module.Id).FirstOrDefault();
                if (string.IsNullOrEmpty(moduleDbOpt.ConnectionString))
                {
                    moduleDbOpt.ConnectionString = dbOptions.ConnectionString;
                    moduleDbOpt.DbType = dbOptions.DbType;
                    moduleDbOpt.IsPintLog = dbOptions.IsPintLog;
                }

                if (string.IsNullOrEmpty(moduleDbOpt.UserKey))
                    moduleDbOpt.UserKey = dbOptions.UserKey;

                var dbContextType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.Name.Equals("DbContext"));
                var filterType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.Name.Equals("QueryFilter"));

                var dbContext = Activator.CreateInstance(dbContextType, moduleDbOpt) as IDbContext;
                dbContext.LoginInfo = loginInfo;

                if (filterType != null)
                    filter = Activator.CreateInstance(filterType, loginInfo, dbContext) as IQueryFilter;

                services.AddSingleton(dbContextType, dbContext);
                services.AddRepositories(module, dbContext, filter);

            }

            return services;
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        private static void AddRepositories(this IServiceCollection services, IModuleDescriptor module, IDbContext dbContext, IQueryFilter filter)
        {
            var interfaceList = module.AssemblyDescriptor.Domain.GetTypes().Where(t => t.FullName != null && t.IsInterface && t.FullName.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)).ToList();

            if (!interfaceList.Any())
                return;

            foreach (var repositoryType in interfaceList)
            {
                var implementType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.FullName != null && m.IsClass && m.FullName.EndsWith("Repository", StringComparison.OrdinalIgnoreCase) && repositoryType.IsAssignableFrom(m));
                if (implementType == null)
                    continue;

                if (filter != null)
                    services.AddSingleton(repositoryType, Activator.CreateInstance(implementType, dbContext, filter));
                else
                    services.AddSingleton(repositoryType, Activator.CreateInstance(implementType, dbContext));
            }
        }

    }
}
