using EasyModular.Auth;
using EasyModular.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo.ModularB.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Text;

namespace Demo.ModularB.Api.Controllers
{ 
    [Description("授权")]
    public class AuthController : ModuleController
    {
        public readonly IJwtHandler _jwtHandler;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IJwtHandler jwtHandler
             , ILogger<AuthController> logger
            )
        {
            _jwtHandler = jwtHandler;
            _logger = logger;
        }


        [HttpGet]
        [Description("获取Token")]
        [AllowAnonymous]
        public JwtTokenModel Token()
        {
            var claims = new[]
            {
                 new Claim(ClaimsName.UserCode, "doordie"),
                 new Claim(ClaimsName.UserName, "doordie")
            };

            return _jwtHandler.Build(claims, Guid.NewGuid().ToString());
        }
    }
}
