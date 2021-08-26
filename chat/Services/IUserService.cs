using chat.Dto;
using chat.DTO;
using chat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Services
{
    interface IUserService
    {
        Task<IEnumerable<User>> AsyncGetAllUsers(QueryPaginationDto query);

        Task<User> AsyncGetOneUser(int id);

        Task<User> AsyncCreateOneUser(CreateUserDto createUserDto);

        Task<User> AsyncUpdateOneUser(int id, UpdateUserDto updateUserDto);

        Task AsyncDeleteOneUser(int id);
    }
}
