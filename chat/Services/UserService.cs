using chat.DAL;
using chat.Dto;
using chat.DTO;
using chat.Entities;
using chat.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Services
{
    public class UserService : IUserService
    {
        ChatContext _context;
        AuthorizationService _authorizationService;

        public UserService(ChatContext context, AuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<CreatedUserDto> AsyncCreateOneUser(CreateUserDto createUserDto)
        {
            User existedUser = await AsyncGetUserInstanceByLogin(createUserDto.Login);

            if (existedUser != null)
            {
                throw new BadRequestException("User with such login already exists");
            }

            User user = new User()
            {
                Description = createUserDto.Description,
                Login = createUserDto.Login,
                Name = createUserDto.Name,
                Password = _authorizationService.GenerateHashFromPassword(createUserDto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            CreatedUserDto response = new CreatedUserDto()
            {
                Id = user.Id,
                Description = user.Description,
                Login = user.Login,
                Name = user.Name
            };

            return response;
        }

        public async Task AsyncDeleteOneUser(int id)
        {
            User user = await AsyncGetUserInstanceById(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetUserDto>> AsyncGetAllUsers(QueryPaginationDto query)
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();
            return users
                .OrderBy(s => s.Id)
                .Skip(query.Skip)
                .Take(query.Take)
                .Select(x => new GetUserDto() { Id = x.Id, Name = x.Name, Description = x.Description });
        }

        public async Task<GetUserDto> AsyncGetOneUser(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return new GetUserDto() { Id = user.Id, Name = user.Name, Description = user.Description };
        }

        public async Task<UpdatedUserDto> AsyncUpdateOneUser(int id, UpdateUserDto updateUserDto)
        {
            if (updateUserDto.Description == null && updateUserDto.Name == null)
            {
                throw new BadRequestException("Nothing to modify");
            }

            User user = await AsyncGetUserInstanceById(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (updateUserDto.Description != null)
            {
                user.Description = updateUserDto.Description;
            }

            if (updateUserDto.Name != null)
            {
                user.Name = updateUserDto.Name;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            UpdatedUserDto response = new UpdatedUserDto()
            {
                Id = user.Id,
                Description = user.Description,
                Name = user.Name
            };

            return response;
        }

        private Task<User> AsyncGetUserInstanceById(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        private Task<User> AsyncGetUserInstanceByLogin(string login)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
