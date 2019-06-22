using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoansManager.BussinesLogic.Dtos.Users;
using LoansManager.BussinesLogic.Infrastructure.SettingsModels;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.Common.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace LoansManager.BussinesLogic.Implementations.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(JwtSettings settings)
        {
            _settings = settings;
        }

        public JwtDto GenerateToken(string userName, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            };

            var expires = now.AddMinutes(_settings.ExpiryMinutes);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expires.ToTimestamp(),
            };
        }
    }
}
