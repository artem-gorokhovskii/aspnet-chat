using chat.DTO;
using chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticateController
    {
        AuthorizationService _authorizationService;

        public AuthenticateController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public Task<string> Authenticate([FromBody] AuthenticateDto authenticateDto)
        {
            return _authorizationService.AsyncAuthenticate(authenticateDto.Login, authenticateDto.Password);
        }
    }
}
