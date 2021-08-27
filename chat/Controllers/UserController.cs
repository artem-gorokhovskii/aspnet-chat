using chat.Dto;
using chat.DTO;
using chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chat.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController
    {
        UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "UserRole")]
        [HttpGet]
        public Task<IEnumerable<GetUserDto>> GetAllUsers([FromQuery] QueryPaginationDto query)
        {
            return _userService.AsyncGetAllUsers(query);
        }

        [Authorize(Policy = "UserRole")]
        [HttpGet("{id}")]
        public Task<GetUserDto> GetUser(int id)
        {
            return _userService.AsyncGetOneUser(id);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPatch("{id}")]
        public Task<UpdatedUserDto> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            return _userService.AsyncUpdateOneUser(id, updateUserDto);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpPost]
        public Task<CreatedUserDto> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            return _userService.AsyncCreateOneUser(createUserDto);
        }

        [Authorize(Policy = "AdminRole")]
        [HttpDelete("{id}")]
        public Task RemoveUser(int id)
        {
            return _userService.AsyncDeleteOneUser(id);
        }
    }
}
