using AutoMapper;
using BLL.API;
using BLL.DTOs;
using DAL.API;
using DAL.Models;

namespace BLL.Services
{
    public class CategoryServiceBLL : ICategoryServiceBLL
    {
        private readonly ICategoryServiceDAL _categoryServiceDAL;
        private readonly ISubCategoryServiceDAL _subcCtegoryServiceDAL;
        private readonly IMapper _mapper;

        public CategoryServiceBLL(ICategoryServiceDAL categoryServiceDAL, IMapper mapper, ISubCategoryServiceDAL subcCtegoryServiceDAL)
        {
            _categoryServiceDAL = categoryServiceDAL;
            _mapper = mapper;
            _subcCtegoryServiceDAL = subcCtegoryServiceDAL;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryServiceDAL.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _categoryServiceDAL.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryServiceDAL.AddAsync(category);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> UpdateAsync(int id, CategoryDTO categoryDto)
        {
            var category = await _categoryServiceDAL.GetByIdAsync(id);
            if (category == null) return false;
            _mapper.Map(categoryDto, category);
            await _categoryServiceDAL.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryServiceDAL.GetByIdAsync(id);
            if (category == null) return false;
            await _categoryServiceDAL.DeleteAsync(category);
            return true;
        }
        public async Task<List<SubCategoryDTO>> GetByCategoryIdAsync(int categoryId)
        {
            var subCategories = await _subcCtegoryServiceDAL.GetByCategoryId(categoryId); 
            return subCategories.Select(sc => _mapper.Map<SubCategoryDTO>(sc)).ToList();
        }

    }
}
