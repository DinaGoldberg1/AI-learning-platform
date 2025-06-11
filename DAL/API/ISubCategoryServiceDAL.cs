using DAL.Models;

namespace DAL.API
{
    public interface ISubCategoryServiceDAL
    {
        Task AddAsync(SubCategory subCategory);
        Task DeleteAsync(SubCategory subCategory);
        Task<List<SubCategory>> GetAllAsync();
        Task<List<SubCategory>> GetByCategoryId(int categoryId);
        Task<SubCategory?> GetByIdAsync(int id);
        Task UpdateAsync(SubCategory subCategory);
    }
}