using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Utils
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加EasyModular工具类库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddEasyModularUtils(this IServiceCollection services)
        {

            services.AddSingleton<IExcelHandler, NpoiExcelHandler>();

            return services;
        }
    }
   
}
