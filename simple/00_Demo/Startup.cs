using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EasyModular;
using EasyModular.Auth;
using EasyModular.AutoMapper;
using EasyModular.Cache;
using EasyModular.FluentValidation;
using EasyModular.Quartz;
using EasyModular.SqlSugar;
using EasyModular.Swagger;
using EasyModular.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using static EasyModular.Utils.SystemTextJsonConvert;

namespace Demo.WebHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc().AddJsonOptions(option => {
                option.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                option.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            });

            services.AddEasyModules();
            services.AddEasyModularAutoMapper();
            services.AddEasyModularSwagger();
            services.AddEasyModularAuth();
            services.AddEasyModularSqlSugar();
            services.AddEasyModularCache();
            services.AddEasyModularQuartz();
            services.AddEasyModularUtils();

            services.AddMvc().AddEasyModularValidators(services).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            //CORS
            services.AddCors(options =>
            {
                /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
                options.AddPolicy("Default",
                    builder => builder.AllowAnyOrigin()
                        .SetPreflightMaxAge(new TimeSpan(0, 30, 0))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
            });
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

            //CORS
            app.UseCors("Default");
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

            app.UseEasyModularShutdownHandler();

            //静态资源配置（目前只开放学习培训模块资源）
            var studyDir = Path.Combine(Path.GetDirectoryName((new Program()).GetType().Assembly.Location), "upload/study");
            if (!Directory.Exists(studyDir))
                Directory.CreateDirectory(studyDir);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(studyDir),
                RequestPath = "/upload/study"
            });
        }
    }
}
