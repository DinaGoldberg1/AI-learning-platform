using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class SubCategoryServiceDAL : ISubCategoryServiceDAL
    {
        private readonly MyDbContext _context;

        public SubCategoryServiceDAL(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SubCategory subCategory)
        {
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }
        public async Task<List<SubCategory>> GetByCategoryId(int categoryId)
        {
            return await _context.SubCategories
                           .Where(sc => sc.CategoryId == categoryId)
                           .ToListAsync();
        }
        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            return await _context.SubCategories.FindAsync(id);
        }

        public async Task UpdateAsync(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();
        }
    }
}
