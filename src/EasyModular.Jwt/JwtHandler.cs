using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EasyModular.Jwt
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;

        public JwtHandler(JwtOptions options)
        {
            _options = options;
        }

        public JwtTokenModel Build(Claim[] claims, string extendData)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(_options.Expires), signingCredentials);

            var model = new JwtTokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = _options.Expires * 60,
                RefreshToken = extendData
            };

            return model;
        }
    }
}
