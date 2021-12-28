using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Admin.Web
{
    /// <summary>
    /// 审计日志处理接口
    /// </summary>
    public interface IAuditingHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="next">请求委托</param>
        /// <returns></returns>
        Task Hand(ActionExecutingContext context, ActionExecutionDelegate next);
    }
}
