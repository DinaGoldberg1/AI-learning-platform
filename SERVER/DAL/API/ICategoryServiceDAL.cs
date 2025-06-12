using DAL.Models;

namespace DAL.API
{
    public interface ICategoryServiceDAL
    {
        Task AddAsync(Category category);
        Task DeleteAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
    }
}