using Demo.Admin.Application.ModulesService;
using Demo.Admin.Application.PermissionService;
using Demo.Admin.Application.ResourceService;
using Demo.Admin.Infrastructure;
using EasyModular;
using EasyModular.Auth;
using EasyModular.SqlSugar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Admin.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //审计日志服务
            services.AddSingleton<IAuditingHandler, AuditingHandler>();
            //权限验证服务
            services.AddScoped<IPermissionValidateHandler, PermissionValidateFilter>();
            //密码处理服务
            services.AddSingleton<IPasswordHandler, Md5PasswordHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            //同步模块
            var modulesService = app.ApplicationServices.GetService<IModulesService>();
            modulesService.Sync();

            //同步接口权限信息
            var permissionService = app.ApplicationServices.GetService<IPermissionService>();
            permissionService.Sync();

            //同步资源数据
            var resourceService = app.ApplicationServices.GetService<IResourceService>();
            resourceService.Sync();

        }

        public void ConfigureMvc(MvcOptions mvcOptions)
        {
            mvcOptions.Filters.Add(typeof(AuditingFilter));
        }
    }
}

