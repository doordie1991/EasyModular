using EasyModular.Auth;
using EasyModular.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo.ModularA.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;
using Demo.ModularA.Application.Test;
using System.Threading.Tasks;
using EasyModular.Utils;

namespace Demo.ModularA.Api.Controllers
{ 
    [Description("授权")]
    public class TestController : ModuleController
    {
        public readonly ITestService _service;
        public TestController(ITestService service)
        {
            _service = service;
        }


        [HttpPost]
        [Description("测试事务")]
        [AllowAnonymous]
        public Task<IResultModel> TestTran()
        {
            return _service.TestTran();
        }
    }
}
