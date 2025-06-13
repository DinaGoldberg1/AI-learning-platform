using BLL.DTOs;

namespace BLL.API
{
    public interface IUserServiceBLL
    {
        Task<UserDTO> CreateAsync(UserDTO userDto);
        Task<bool> DeleteAsync(string id);
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<UserDTO?> GetByUserIdAsync(string userId);
        Task<UserDTO?> GetByNameAndPhoneAsync(UserDTO userDTO);
        Task<bool> UpdateAsync(string id, UserDTO userDto);
    }
}