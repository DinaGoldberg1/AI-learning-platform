using BLL.DTOs;

namespace BLL.API
{
    public interface ISubCategoryServiceBLL
    {
        Task<SubCategoryDTO> CreateAsync(SubCategoryDTO subCategoryDto);
        Task<bool> DeleteAsync(int id);
        Task<List<SubCategoryDTO>> GetAllAsync();
        Task<SubCategoryDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, SubCategoryDTO subCategoryDto);
    }
}