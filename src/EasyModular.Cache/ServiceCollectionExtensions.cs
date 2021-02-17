using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using EasyModular.Utils.Helpers;

namespace EasyModular.Cache
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IServiceCollection AddEasyModularCache(this IServiceCollection services)
        {
            var cacheOptions = ConfigHelper.GetModel<CacheOptions>(Path.Combine(AppContext.BaseDirectory, "config/cache.json"));
            services.AddSingleton(cacheOptions);

            switch(cacheOptions.Mode)
            {
                case CacheMode.MemoryCache:
                    services.AddMemoryCache();
                    services.AddSingleton<ICacheHandler, MemoryCacheHandler>();
                    break;

                case CacheMode.Redis:
                    var redisHelper = new RedisHelper(cacheOptions.Redis);
                    services.AddSingleton(redisHelper);
                    services.AddSingleton<ICacheHandler, RedisCacheHandler>();
                    break;
            }

            return services;
        }
    }
}
