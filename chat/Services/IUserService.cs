using chat.Dto;
using chat.DTO;
using chat.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chat.Services
{
    interface IUserService
    {
        Task<IEnumerable<User>> AsyncGetAllUsers(QueryPaginationDto query);

        Task<User> AsyncGetOneUser(int id);

        Task<CreatedUserDto> AsyncCreateOneUser(CreateUserDto createUserDto);

        Task<UpdatedUserDto> AsyncUpdateOneUser(int id, UpdateUserDto updateUserDto);

        Task AsyncDeleteOneUser(int id);
    }
}
