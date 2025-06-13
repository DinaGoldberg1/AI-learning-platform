using AutoMapper;
using BLL.API;
using BLL.DTOs;
using DAL.API;
using DAL.Models;

namespace BLL.Services
{
    public class UserServiceBLL : IUserServiceBLL
    {
        private readonly IUserServiceDAL _userServiceDAL;
        private readonly IMapper _mapper;

        public UserServiceBLL(IUserServiceDAL userServiceDAL, IMapper mapper)
        {
            _userServiceDAL = userServiceDAL;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userServiceDAL.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userServiceDAL.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> GetByUserIdAsync(string userId)
        {
            var user = await _userServiceDAL.GetByUserIdAsync(userId);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO?> GetByNameAndPhoneAsync(UserDTO userDTO)
        {
            var user = await _userServiceDAL.GetByNameAndPhoneAsync(userDTO.Name,userDTO.Phone);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> CreateAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userServiceDAL.AddAsync(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UpdateAsync(string id, UserDTO userDto)
        {
            var user = await _userServiceDAL.GetByUserIdAsync(id);
            if (user == null) return false;
            _mapper.Map(userDto, user);
            await _userServiceDAL.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userServiceDAL.GetByUserIdAsync(id);
            if (user == null) return false;
            await _userServiceDAL.DeleteAsync(user);
            return true;
        }
    }
}