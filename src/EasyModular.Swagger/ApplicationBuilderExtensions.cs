using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace EasyModular.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 自定义Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEasyModularSwagger(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetService<IList<IModuleDescriptor>>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (modules == null) return;

                foreach (var moduleInfo in modules)
                {
                    if (((ModuleDescriptor)moduleInfo).Initializer == null)
                        continue;

                    c.SwaggerEndpoint($"/swagger/{moduleInfo.Id}/swagger.json", moduleInfo.Name);
                }
            });

            return app;
        }
    }
}
