using BLL.DTOs;

namespace BLL.API
{
    public interface IPromptServiceBLL
    {
        Task<bool> DeleteAsync(int id);
        Task<List<PromptDTO>> GetAllAsync();
        Task<PromptDTO?> GetByIdAsync(int id);
        Task<string> ProcessPromptAsync(PromptDTO promptDto, int userId);
        Task<List<PromptDTO>> GetUserHistoryAsync(int userId);
        Task<bool> UpdateAsync(int id, PromptDTO promptDto);
    }
}