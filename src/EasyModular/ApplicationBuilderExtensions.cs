using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EasyModular
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEasyModules(this IApplicationBuilder app, IHostEnvironment env)
        {
            var modules = app.ApplicationServices.GetService<IList<IModuleDescriptor>>();
            foreach (var module in modules)
            {
                //加载模块初始化器
                ((ModuleDescriptor)module).Initializer?.Configure(app, env);
            }

            return app;
        }

        /// <summary>
        /// 启用应用停止处理
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseEasyModularShutdownHandler(this IApplicationBuilder app)
        {
            var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            lifeTime.ApplicationStopping.Register(() =>
            {
                var handlers = app.ApplicationServices.GetServices<IAppShutdownHandler>().ToList();
                foreach (var handler in handlers)
                    handler.Handle();
            });

            return app;
        }
    }
}
