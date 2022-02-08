using CarCareApplication.Core.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarCareApplication.WebApp.Server.Utility
{
    public static class TokenManager
    {
        public static string GenerateToken(IConfiguration Configuration, User model, DateTime expires, DateTime dontAcceptbefore)
        {
            byte[] Key = Convert.FromBase64String(Configuration["Jwt:Key"]);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Key);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,model.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()), // Store user Id
                    new Claim(ClaimTypes.Name, model.FirstName),
                    new Claim(ClaimTypes.Role, model.Role.Name), // Store Role Name 
                }),
                Issuer = Configuration["Jwt:Issuer"],//The system which created JWT
                Audience = Configuration["Jwt:Audience"],//The System will use JWT
                IssuedAt = DateTime.UtcNow, // claim identifies the time at which the JWT was issued.
                NotBefore = dontAcceptbefore, // Don't accept jwt before this time
                Expires = expires, // example, DateTime.UtcNow.AddMinutes(30)
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
