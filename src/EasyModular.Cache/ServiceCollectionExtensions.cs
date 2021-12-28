using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using EasyModular.Utils;

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
            var sp = services.BuildServiceProvider();
            var webCofing = sp.GetService<WebConfigModel>();

            switch (webCofing.Cache.Mode)
            {
                case CacheMode.MemoryCache:
                    services.AddMemoryCache();
                    services.AddSingleton<ICacheHandler, MemoryCacheHandler>();
                    break;

                case CacheMode.Redis:
                    var redisHelper = new RedisHelper(webCofing.Cache.Prefix, webCofing.Cache.ConnectionString);
                    services.AddSingleton(redisHelper);
                    services.AddSingleton<ICacheHandler, RedisCacheHandler>();
                    break;
            }

            return services;
        }
    }
}
