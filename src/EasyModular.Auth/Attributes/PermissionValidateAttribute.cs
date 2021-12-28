using EasyModular.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace EasyModular.Auth
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionValidateAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //排除匿名访问
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
                return;

            var config = context.HttpContext.RequestServices.GetService(typeof(WebConfigModel)) as WebConfigModel;
            if (config != null)
            {
                //是否开启权限认证
                if (!config.IsValidate)
                    return;

                //是否启用单账户登录
                if (config.IsSingleAccount)
                {
                    var singleAccountLoginHandler = context.HttpContext.RequestServices.GetService(typeof(ISingleAccountLoginHandler)) as ISingleAccountLoginHandler;
                    if (singleAccountLoginHandler != null && singleAccountLoginHandler.Validate().GetAwaiter().GetResult())
                    {
                        context.Result = new ContentResult();
                        context.HttpContext.Response.StatusCode = 622;//自定义状态码来判断是否是在其他地方登录
                        return;
                    }
                }
            }

            //未登录
            var loginInfo = context.HttpContext.RequestServices.GetService(typeof(ILoginInfo)) as ILoginInfo;
            if (loginInfo == null || string.IsNullOrEmpty(loginInfo.UserName))
            {
                context.Result = new ChallengeResult();
                return;
            }

            //排除通用接口
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(CommonAttribute)))
                return;

            var handler = context.HttpContext.RequestServices.GetService(typeof(IPermissionValidateHandler)) as IPermissionValidateHandler;
            if (!handler.Validate(context.ActionDescriptor.RouteValues, context.HttpContext.Request.Method))
            {
                //无权访问
                context.Result = new ForbidResult();
            }
        }
    }
}
