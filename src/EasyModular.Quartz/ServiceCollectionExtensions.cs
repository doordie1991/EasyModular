using EasyModular.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace EasyModular.Quartz
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <param name="options">调度配置</param>
        /// <returns></returns>
        public static IServiceCollection AddEasyModularQuartz(this IServiceCollection services)
        {
            var opts = ConfigHelper.GetModel<QuartzOptions>(Path.Combine(AppContext.BaseDirectory, "config/quartz.json"));
            if (opts == null)
                return services;

            services.AddSingleton(opts);

            var sp = services.BuildServiceProvider();
            var modules = sp.GetService<IList<IModuleDescriptor>>();

            if (!modules.Any())
                return services;

            #region ==反射注入所有模块的任务服务==

            foreach (var module in modules)
            {
                var descriptor = new QuartzModuleDescriptor
                {
                    Module = module
                };

                var quartzAssembly = module.AssemblyDescriptor.Quartz;
                if (quartzAssembly == null)
                    continue;

                var taskTypes = module.AssemblyDescriptor.Quartz.GetTypes().Where(m => typeof(ITask).IsAssignableFrom(m));
                foreach (var taskType in taskTypes)
                {
                    var taskDescriptor = new QuartzTaskDescriptor
                    {
                        Name = taskType.Name,
                        Class = $"{taskType.FullName}, {taskType.Assembly.GetName().Name}"
                    };

                    var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(taskType, typeof(DescriptionAttribute));
                    if (descAttr != null && descAttr.Description!=null)
                        taskDescriptor.Name = descAttr.Description;

                    descriptor.Tasks.Add(taskDescriptor);

                    services.AddTransient(taskType);
                }

            }

            #endregion

            //注入Quartz服务实例
            services.AddSingleton<IQuartzServer, QuartzServer>();

            //注入日志
            services.TryAddTransient<ITaskLogger, TaskLogger>();

            //注入应用关闭事件
            services.AddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            return services;
        }
    }
}
