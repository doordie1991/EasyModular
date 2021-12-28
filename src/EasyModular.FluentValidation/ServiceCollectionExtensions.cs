using EasyModular.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyModular.FluentValidation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加FluentValidation
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMvcBuilder AddEasyModularValidators(this IMvcBuilder builder, IServiceCollection services)
        {
            builder.AddFluentValidation(fv =>
            {
                var modules = services.BuildServiceProvider().GetService<IList<IModuleDescriptor>>();
                foreach (var module in modules)
                {
                    if (module.AssemblyDescriptor != null && module.AssemblyDescriptor is ModuleAssemblyDescriptor descriptor)
                    {
                        if (descriptor.Web != null)
                        {
                            fv.RegisterValidatorsFromAssembly(descriptor.Web);
                        }
                    }
                }
            });

            //当一个验证失败时，后续的验证不再执行
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;


            //重写modelstate，格式化验证失败时返回时数据格式 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors
                                    .Select(p => p.ErrorMessage))
                        .ToList();

                    return new JsonResult(ResultModel.Failed(string.Join("|", errors)));
                };
            });
            return builder;
        }
    }
}
