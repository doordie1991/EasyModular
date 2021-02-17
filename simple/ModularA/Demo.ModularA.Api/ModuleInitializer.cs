using EasyModular;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.ModularA.Api
{
    public class ModuleInitializer : IModuleInitializer
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {

        }
    }
}
