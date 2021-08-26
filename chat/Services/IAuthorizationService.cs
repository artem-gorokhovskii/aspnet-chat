using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Services
{
    interface IAuthorizationService
    {
        public string GenerateHashFromPassword(string password);
    }
}
