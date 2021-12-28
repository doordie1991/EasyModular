using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EasyModular.Auth
{
    public interface IJwtHandler
    {
        JwtTokenModel Build(Claim[] claims, string refreshToken = null);
    }
}
