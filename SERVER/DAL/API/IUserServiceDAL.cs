using DAL.Models;

namespace DAL.API
{
    public interface IUserServiceDAL
    {
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUserIdAsync(string userId);
        Task<User?> GetByNameAndPhoneAsync(string name, string phone);
        Task UpdateAsync(User user);
    }
}