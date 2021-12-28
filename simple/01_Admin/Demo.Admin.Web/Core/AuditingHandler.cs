using EasyModular.Utils;
using Demo.Admin.Application;
using Demo.Admin.Application.AuditInfoService;
using Demo.Admin.Domain;
using Demo.Admin.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyModular.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Admin.Web
{
    /// <summary>
    /// 审计日志处理
    /// </summary>
    public class AuditingHandler : IAuditingHandler
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAuditInfoService _auditInfoService;
        private readonly ILogger _logger;
        private readonly MvcHelper _mvcHelper;

        public AuditingHandler(ILoginInfo loginInfo, IAuditInfoService auditInfoService, ILogger<AuditingHandler> logger, ApplicationPartManager partManager)
        {
            _loginInfo = loginInfo;
            _auditInfoService = auditInfoService;
            _logger = logger;
            _mvcHelper = new MvcHelper(partManager);
        }

        public async Task Hand(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auditInfo = CreateAuditInfo(context);

            var sw = new Stopwatch();
            sw.Start();

            var resultContext = await next();

            sw.Stop();

            if (auditInfo != null)
            {
                try
                {
                    //执行结果
                    var str = (resultContext.Result as ObjectResult)?.Value.ToJson();
                    if (str != null)
                        auditInfo.Result = str.Length > 12000 ? str.Substring(0, 12000) : str;

                    //用时
                    auditInfo.ExecutionDuration = sw.ElapsedMilliseconds;

                    await _auditInfoService.Add(auditInfo);
                }
                catch (Exception ex)
                {
                    _logger.LogError("审计日志插入异常：{@ex}", ex);
                }
            }
        }

        private AuditInfoAddModel CreateAuditInfo(ActionExecutingContext context)
        {
            try
            {
                var routeValues = context.ActionDescriptor.RouteValues;
                var auditInfo = new AuditInfoAddModel
                {
                    Area = routeValues["area"] ?? "",
                    Controller = routeValues["controller"],
                    Action = routeValues["action"],
                    Parameters = context.ActionArguments.ToJson(),
                    IP = _loginInfo.IP
                };

                var controllerDescriptor = _mvcHelper.GetAllController().FirstOrDefault(m => m.Area.NotNull() && m.Area.EqualsIgnoreCase(auditInfo.Area) && m.Name.EqualsIgnoreCase(auditInfo.Controller));
                if (controllerDescriptor != null)
                {
                    auditInfo.ControllerDesc = controllerDescriptor.Description;

                    var actionDescription = _mvcHelper.GetAllAction().FirstOrDefault(m => m.Controller == controllerDescriptor && m.Name.EqualsIgnoreCase(auditInfo.Action));
                    if (actionDescription != null)
                    {
                        auditInfo.ActionDesc = actionDescription.Description;
                    }
                }

                auditInfo.BrowserInfo = context.HttpContext.Request.Headers["User-Agent"];

                return auditInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError("审计日志创建异常：{@ex}", ex);
            }

            return null;
        }
    }
}
