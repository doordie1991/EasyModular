using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyModular;
using EasyModular.Auth;
using EasyModular.AutoMapper;
using EasyModular.Cache;
using EasyModular.FluentValidation;
using EasyModular.Jwt;
using EasyModular.SqlSugar;
using EasyModular.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.ModularA.WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                //设置日期格式化格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            services.AddEasyModules();
            services.AddEasyModularAutoMapper();

            services.AddEasyModularSwagger();
            services.AddEasyModularJwt();
            services.AddEasyModularAuth();
            services.AddEasyModularSqlSugar();
            services.AddEasyModularCache();

            services.AddMvc().AddEasyModularValidators(services).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //认证
            app.UseAuthentication();
            //授权
            app.UseAuthorization();
            //全局异常处理
            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEasyModules(env);

            app.UseEasyModularSwagger();
        }
    }
}
