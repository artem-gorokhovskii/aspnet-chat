using chat.Dto;
using chat.DTO;
using chat.Entities;
using chat.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public Task<IEnumerable<User>> GetAllUsers([FromQuery] QueryPaginationDto query)
        {
            return _userService.AsyncGetAllUsers(query);
        }

        [HttpGet("{id}")]
        public Task<User> GetUser(int id)
        {
            return _userService.AsyncGetOneUser(id);
        }

        [HttpPost]
        public Task<User> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            return _userService.AsyncCreateOneUser(createUserDto);
        }

        [HttpDelete("{id}")]
        public Task RemoveUser(int id)
        {
            return _userService.AsyncDeleteOneUser(id);
        }

        [HttpPatch("{id}")]
        public Task<User> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            return _userService.AsyncUpdateOneUser(id, updateUserDto);
        }
    }
}
