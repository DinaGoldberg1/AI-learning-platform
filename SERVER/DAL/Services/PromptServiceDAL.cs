using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class PromptServiceDAL : IPromptServiceDAL
    {
        private readonly MyDbContext _context;

        public PromptServiceDAL(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Prompt prompt)
        {
            _context.Prompts.Add(prompt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Prompt prompt)
        {
            _context.Prompts.Remove(prompt);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Prompt>> GetAllAsync()
        {
            return await _context.Prompts.ToListAsync();
        }

        public async Task<Prompt?> GetByIdAsync(int id)
        {
            return await _context.Prompts.FindAsync(id);
        }

        public async Task UpdateAsync(Prompt prompt)
        {
            _context.Prompts.Update(prompt);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Prompt>> GetAllPromptsAsync()
        {
            return await _context.Prompts.ToListAsync();
        }

        public async Task<List<Prompt>> GetUserHistoryAsync(int userId)
        {
            return await _context.Prompts
                .Where(prompt => prompt.UserId == userId)
                .OrderByDescending(prompt => prompt.CreatedAt)
                .ToListAsync();
        }

    }
}
