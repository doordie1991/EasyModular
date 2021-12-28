using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace EasyModular
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IList<IModuleDescriptor> AddEasyModules(this IServiceCollection services)
        {
            using var jsonReader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "config/web.json"));
            var webConfig = JsonSerializer.Deserialize<WebConfigModel>(jsonReader.ReadToEnd(), new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            });
            services.AddSingleton(webConfig);

            IModuleHandler moduleHandler = new ModuleHandler();
            var modules = moduleHandler.LoadModules();

            services.AddSingleton<IList<IModuleDescriptor>>(modules);

            services.InitializeModule(modules);

            services.AddModuleApplicationServices(modules);

            return modules;
        }

        /// <summary>
        /// 初始化模块
        /// </summary>
        private static void InitializeModule(this IServiceCollection services, IList<IModuleDescriptor> modules)
        {

            foreach (var module in modules)
            {
                //注入各模块服务
                ((ModuleDescriptor)module).Initializer?.ConfigureServices(services);

                //模块中的MVC配置
                services.AddMvcCore(c =>
                {
                    ((ModuleDescriptor)module).Initializer?.ConfigureMvc(c);
                })
                .AddApplicationPart(module.AssemblyDescriptor.Web);
            }
        }

        /// <summary>
        /// 添加模块应用服务
        /// </summary>
        private static void AddModuleApplicationServices(this IServiceCollection services, IList<IModuleDescriptor> modules)
        {
            foreach (var module in modules)
            {
                if (module.AssemblyDescriptor == null || module.AssemblyDescriptor.Application == null)
                    return;

                var types = module.AssemblyDescriptor.Application.GetTypes();
                var interfaces = types.Where(t => t.FullName != null && t.IsInterface && t.FullName.EndsWith("Service", StringComparison.OrdinalIgnoreCase));
                foreach (var serviceType in interfaces)
                {
                    var implementationType = types.FirstOrDefault(m => m != serviceType && serviceType.IsAssignableFrom(m));
                    if (implementationType != null)
                    {
                        services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton));
                    }
                }
            }
        }

    }
}
