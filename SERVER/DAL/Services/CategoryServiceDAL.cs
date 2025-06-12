using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class CategoryServiceDAL : ICategoryServiceDAL
    {
        private readonly MyDbContext _context;

        public CategoryServiceDAL(MyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        //public async Task<List<SubCategoryDTO>> GetByCategoryIdAsync(int categoryId)
        //{
        //    var subCategories = await _context.SubCategories
        //        .Where(sc => sc.CategoryId == categoryId)
        //        .ToListAsync();

        //    return _mapper.Map<List<SubCategoryDTO>>(subCategories);
        //}

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
