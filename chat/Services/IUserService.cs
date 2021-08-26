using chat.Dto;
using chat.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chat.Services
{
    interface IUserService
    {
        Task<IEnumerable<GetUserDto>> AsyncGetAllUsers(QueryPaginationDto query);

        Task<GetUserDto> AsyncGetOneUser(int id);

        Task<CreatedUserDto> AsyncCreateOneUser(CreateUserDto createUserDto);

        Task<UpdatedUserDto> AsyncUpdateOneUser(int id, UpdateUserDto updateUserDto);

        Task AsyncDeleteOneUser(int id);
    }
}
