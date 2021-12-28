using EasyModular.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyModular.Jwt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Jwt认证
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddEasyModularJwt(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var webCofing = sp.GetService<WebConfigModel>();

            services.TryAddSingleton<IJwtHandler, JwtHandler>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = webCofing.Jwt.Issuer,
                        ValidAudience = webCofing.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(webCofing.Jwt.Key)),
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            return services;
        }
    }
}
