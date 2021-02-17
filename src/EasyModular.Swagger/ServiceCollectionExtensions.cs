﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyModular.Swagger
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddEasyModularSwagger(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var modules = sp.GetService<IList<IModuleDescriptor>>();

            services.AddSwaggerGen(c =>
            {
                if (modules != null)
                {
                    foreach (var moduleInfo in modules)
                    {
                        if (((ModuleDescriptor)moduleInfo).Initializer == null)
                            continue;

                        c.SwaggerDoc(moduleInfo.Id, new OpenApiInfo
                        {
                            Title = moduleInfo.Name,
                            Version = moduleInfo.Version
                        });
                    }
                }

                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                };

            //添加设置Token的按钮
            c.AddSecurityDefinition("Bearer", securityScheme);

            //添加Jwt验证设置
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                    });

            //链接转小写过滤器
            c.DocumentFilter<LowercaseDocumentFilter>();

            //描述信息处理
            c.DocumentFilter<DescriptionDocumentFilter>();

            //隐藏属性
            c.SchemaFilter<IgnorePropertySchemaFilter>();
            });

            //添加API分组约定
            services.AddMvc(c =>
            {
                c.Conventions.Add(new ApiExplorerGroupConvention());
            });

            return services;
        }
    }
}
