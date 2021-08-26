using chat.DAL;
using chat.Dto;
using chat.DTO;
using chat.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Services
{
    public class UserService : IUserService
    {
        ChatContext _context;
        public UserService(ChatContext context)
        {
            _context = context;
        }

        public async Task<User> AsyncCreateOneUser(CreateUserDto createUserDto)
        {
            User user = new User()
            {
                Description = createUserDto.Description,
                Login = createUserDto.Login,
                Name = createUserDto.Name,
                Password = createUserDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task AsyncDeleteOneUser(int id)
        {
            User user = await AsyncGetOneUser(id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> AsyncGetAllUsers(QueryPaginationDto query)
        {
            var users = await _context.Users.ToListAsync();
            return users.OrderBy(s => s.Id).Skip(query.Skip).Take(query.Take);
        }

        public Task<User> AsyncGetOneUser(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> AsyncUpdateOneUser(int id, UpdateUserDto updateUserDto)
        {
            if (updateUserDto.Description == null && updateUserDto.Name == null)
            {
                throw new ArgumentException("Nothing to modify");
            }

            User user = await AsyncGetOneUser(id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
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
            return user;
        }
    }
}
