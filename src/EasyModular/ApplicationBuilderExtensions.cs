using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
