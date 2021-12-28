using EasyModular;
using EasyModular.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Scheduling.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //日志
            services.AddSingleton<ITaskLogger, TaskLogger>();
            //监听
            services.AddSingleton<ISchedulerListener, SchedulerListener>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            var server = app.ApplicationServices.GetService<IQuartzServer>();
            var options = app.ApplicationServices.GetService<QuartzOptions>();

            if (options.Enabled)
                server.Start();
        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {

        }
    }
}

