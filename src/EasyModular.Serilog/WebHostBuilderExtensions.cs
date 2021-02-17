using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular.Serilog
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseEasyModularSerilog(this IWebHostBuilder builder)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "config");
            if (!Directory.Exists(filePath))
                return builder;

            var logBuilder = new ConfigurationBuilder()
                .SetBasePath(filePath)
                .AddJsonFile("log.json", true, true);

            var cfg = logBuilder.Build();

            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                if (cfg != null)
                {
                    loggerConfiguration.ReadFrom.Configuration(cfg);
                }

                loggerConfiguration.Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
