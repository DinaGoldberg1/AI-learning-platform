using DAL.Models;

namespace DAL.API
{
    public interface IPromptServiceDAL
    {
        Task AddAsync(Prompt prompt);
        Task DeleteAsync(Prompt prompt);
        Task<List<Prompt>> GetAllAsync();
        Task<Prompt?> GetByIdAsync(int id);
        Task UpdateAsync(Prompt prompt);
    }
}