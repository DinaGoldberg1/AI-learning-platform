using BLL.DTOs;

namespace BLL.API
{
    public interface ICategoryServiceBLL
    {
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDto);
        Task<List<SubCategoryDTO>> GetByCategoryIdAsync(int categoryId);
        Task<bool> DeleteAsync(int id);
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, CategoryDTO categoryDto);
    }
}