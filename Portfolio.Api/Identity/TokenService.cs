using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Api.Identity
{
    public class TokenService
    {
        private readonly SymmetricSecurityKey _signingKey;
        private readonly string _issuer;

        public TokenService(IConfiguration configuration)
        {
            string signingKey = configuration["Jwt:SigningKey"];
            if (string.IsNullOrWhiteSpace(signingKey))
            {
                throw new InvalidOperationException("Jwt:SigningKey configuration value is required.");
            }

            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            _issuer = configuration["Jwt:Issuer"] ?? "portfolio-api";
        }

        public string Issuer => _issuer;

        public (string token, DateTime expiresAtUtc) CreateAccessToken(IEnumerable<Claim> claims, TimeSpan lifetime)
        {
            DateTime expiresAtUtc = DateTime.UtcNow.Add(lifetime);

            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _issuer,
                audience: "resource_gateway",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAtUtc,
                signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return (token, expiresAtUtc);
        }

        public TokenValidationParameters GetValidationParameters() => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingKey,
            ClockSkew = TimeSpan.FromSeconds(30)
        };

        public static string GenerateOpaqueToken()
        {
            byte[] bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
