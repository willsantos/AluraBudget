using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UsersApi.Services
{
    public class TokenService
    {
        public Token CreateToken(IdentityUser<int> user, string role)
        {
            Claim[] userRights = new Claim[]
            {
                new Claim("username",user.UserName),
                new Claim("id",user.Id.ToString()),
                new Claim(ClaimTypes.Role,role)
            };

            var hashKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("bb968550154802c0849a8f092bcf5cf3f978b9ea2597b73da7ff0c8393e2e800")
            );

            var credentials = new SigningCredentials(hashKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userRights,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);

        }
    }
}
