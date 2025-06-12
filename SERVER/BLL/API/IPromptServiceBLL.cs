using BLL.DTOs;

namespace BLL.API
{
    public interface IPromptServiceBLL
    {
        Task<PromptDTO> CreateAsync(PromptDTO promptDto);
        Task<bool> DeleteAsync(int id);
        Task<List<PromptDTO>> GetAllAsync();
        Task<PromptDTO?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, PromptDTO promptDto);
    }
}