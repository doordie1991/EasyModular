using EasyModular;
using EasyModular.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Web
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {
        private readonly IAuditingHandler _handler;
        private readonly WebConfigModel _config;

        public AuditingFilter(IAuditingHandler handler, WebConfigModel config)
        {
            _handler = handler;
            _config = config;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!_config.IsAudit||context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(DisableAuditingAttribute)))
            {
                return next();
            }

            return _handler.Hand(context, next);
        }
    }
}
