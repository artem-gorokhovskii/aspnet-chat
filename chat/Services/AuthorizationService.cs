using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace chat.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _config;
        private readonly string salt = "";

        public AuthorizationService(IConfiguration config)
        {
            _config = config;
            salt = config.GetSection("ChatApp")["PasswordSalt"];
        }

        public string GenerateHashFromPassword(string password)
        {
            using SHA256Managed sha256 = new SHA256Managed();

            byte[] mixedPassword = Encoding.UTF8.GetBytes(salt + password);
            return BitConverter.ToString(sha256.ComputeHash(mixedPassword)).Replace("-","");
        }
    }
}
