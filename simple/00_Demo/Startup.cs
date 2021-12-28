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
                /*�������ͬԴ���ԣ����ǳ��ڰ�ȫ���ǣ�����������ƴӽű�����Ŀ���HTTP���󣨱����첽����GET, POST, PUT, DELETE, OPTIONS�ȵȣ�
                �������������������ķ����������������󣬵�һ���������ʹ��OPTIONS��������һ��Ԥ�����󣬵ڶ��β����������첽����
                ��һ�ε�Ԥ�������֪�������Ƿ�����ÿ���������������ŷ���ڶ�����ʵ��������������������صڶ�������
                Access-Control-Max-Age����ָ������Ԥ���������Ч�ڣ���λΪ�룬���ڴ��ڼ䲻�÷�����һ��Ԥ������*/
                options.AddPolicy("Default",
                    builder => builder.AllowAnyOrigin()
                        .SetPreflightMaxAge(new TimeSpan(0, 30, 0))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition"));//�����ļ�ʱ���ļ����ƻᱣ����headers��Content-Disposition��������
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
            //��֤
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();

            //ȫ���쳣����
            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEasyModules(env);

            app.UseEasyModularSwagger();

            app.UseEasyModularShutdownHandler();

            //��̬��Դ���ã�Ŀǰֻ����ѧϰ��ѵģ����Դ��
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
