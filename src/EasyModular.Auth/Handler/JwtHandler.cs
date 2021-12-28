using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EasyModular.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly WebConfigModel _options;

        public JwtHandler(WebConfigModel options)
        {
            _options = options;
        }

        public JwtTokenModel Build(Claim[] claims, string refreshToken = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Jwt.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_options.Jwt.Issuer, _options.Jwt.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(_options.Jwt.Expires), signingCredentials);

            var model = new JwtTokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = _options.Jwt.Expires * 60,
                RefreshToken = refreshToken
            };

            return model;
        }
    }
}
