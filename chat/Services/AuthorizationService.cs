using chat.DAL;
using chat.Entities;
using chat.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chat.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _config;
        private readonly string salt = "";
        ChatContext _context;

        public AuthorizationService(IConfiguration config, ChatContext context)
        {
            _config = config;
            _context = context;

            salt = config.GetSection("ChatApp")["PasswordSalt"];
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

            return "Token";
        }
    }
}
