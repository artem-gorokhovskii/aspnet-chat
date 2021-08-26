using chat.DAL;
using chat.Entities;
using chat.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chat.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _config;
        private readonly string salt = "";
        private readonly string authTokenSecret = "";
        private readonly int authTokenLifetime = 60 * 24; // 1 day
        private readonly string authTokenIssuer = "";
        private readonly string authTokenAudience = "";

        ChatContext _context;

        public AuthorizationService(IConfiguration config, ChatContext context)
        {
            _config = config;
            _context = context;

            salt = config.GetSection("ChatApp")["PasswordSalt"];
            authTokenSecret = config.GetSection("ChatApp")["AuthTokenSecret"];
            authTokenLifetime = int.Parse(config.GetSection("ChatApp")["AuthTokenLifetime"]);
            authTokenIssuer = config.GetSection("ChatApp")["AuthTokenIssuer"];
            authTokenAudience = config.GetSection("ChatApp")["AuthTokenAudience"];
        }

        public string GenerateHashFromPassword(string password)
        {
            using SHA256Managed sha256 = new SHA256Managed();

            byte[] mixedPassword = Encoding.UTF8.GetBytes(salt + password);
            return BitConverter.ToString(sha256.ComputeHash(mixedPassword)).Replace("-", "");
        }

        public async Task<string> AsyncAuthenticate(string login, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
            if (user == null)
            {
                throw new UnauthorizedException("Invalid login or password");
            }

            string hashPassword = GenerateHashFromPassword(password);

            if (user.Password != hashPassword)
            {
                throw new UnauthorizedException("Invalid login or password");
            }

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authTokenSecret));

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: authTokenIssuer,
                    audience: authTokenAudience,
                    notBefore: now,
                    claims: GetIdentity(user).Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authTokenLifetime)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );
            return claimsIdentity;
        }
    }
}
