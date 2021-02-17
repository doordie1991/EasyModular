using EasyModular.Auth;
using EasyModular.Utils.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var dbOptions = ConfigHelper.GetModels<DbOptions>(Path.Combine(AppContext.BaseDirectory, "config/db.json"));

            if (dbOptions == null)
                return services;

            services.AddSingleton(dbOptions);

            var sp = services.BuildServiceProvider();
            var modules = sp.GetService<IList<IModuleDescriptor>>();
            var loginInfo = sp.GetService<ILoginInfo>();

            foreach (var module in modules)
            {
                if (module.AssemblyDescriptor.Domain == null)
                    continue;

                var dbOption = dbOptions.Where(m => m.ModuleId == module.Id).FirstOrDefault();
                var dbContextType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.Name.Equals("DbContext"));

                var dbContext = (IDbContext)Activator.CreateInstance(dbContextType, dbOption);
                dbContext.LoginInfo = loginInfo;

                services.AddSingleton(dbContextType, dbContext);
                services.AddRepositories(module, dbContext);
            }

            return services;
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        private static void AddRepositories(this IServiceCollection services, IModuleDescriptor module, IDbContext dbContext)
        {
            var interfaceList = module.AssemblyDescriptor.Domain.GetTypes().Where(t => t.FullName != null && t.IsInterface && t.FullName.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)).ToList();

            if (!interfaceList.Any())
                return;

            foreach (var repositoryType in interfaceList)
            {
                var implementType = module.AssemblyDescriptor.Infrastructure.GetTypes().FirstOrDefault(m => m.FullName != null && m.IsClass && m.FullName.EndsWith("Repository", StringComparison.OrdinalIgnoreCase) && repositoryType.IsAssignableFrom(m));
                if (implementType != null)
                {
                    services.AddSingleton(repositoryType, Activator.CreateInstance(implementType, dbContext));
                }
            }
        }
    }
}
