using EasyModular.Utils.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular.Auth
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Auth
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddEasyModularAuth(this IServiceCollection services)
        {
            var permissionConfig = ConfigHelper.GetModel<PermissionConfigModel>(Path.Combine(AppContext.BaseDirectory, "config/permission.json"));

            if (permissionConfig == null)
                return services;

            services.AddSingleton(permissionConfig);
            services.TryAddSingleton<ILoginInfo, LoginInfo>();

            return services;
        }
    }
}
