using AutoMapper;
using LibraryManagementApi.Dto;
using LibraryManagementApi.Helpers_Extensions;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Services
{


    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> CreateUser(UserDto userCreateDto);
        Task<bool> UpdateUser(int id, UserDto userUpdateDto);
        Task<bool> DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUser(UserDto userCreateDto)
        {
            userCreateDto.Password = userCreateDto.Password.ToMD5Hash();
            var user = _mapper.Map<User>(userCreateDto);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateUser(int id, UserDto userUpdateDto)
        {
            userUpdateDto.Password = userUpdateDto.Password.ToMD5Hash();
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _mapper.Map(userUpdateDto, user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }

}
