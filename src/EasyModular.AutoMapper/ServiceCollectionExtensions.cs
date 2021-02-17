using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyModular.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddEasyModularAutoMapper(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var modules = sp.GetService<IList<IModuleDescriptor>>();

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var moduleInfo in modules)
                {
                    if (moduleInfo.AssemblyDescriptor.Application == null)
                        continue;

                    var types = moduleInfo.AssemblyDescriptor.Application.GetTypes().Where(t => typeof(IMapperConfig).IsAssignableFrom(t));

                    foreach (var type in types)
                    {
                        ((IMapperConfig)Activator.CreateInstance(type)).Bind(cfg);
                    }
                }
            });

            services.AddSingleton(config.CreateMapper());

            return services;
        }
    }
}
