using AutoMapper;
using BLL.API;
using BLL.DTOs;
using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class SubCategoryServiceBLL : ISubCategoryServiceBLL
    {
        private readonly ISubCategoryServiceDAL _subCategoryServiceDAL;
        private readonly IMapper _mapper;

        public SubCategoryServiceBLL(ISubCategoryServiceDAL subCategoryServiceDAL, IMapper mapper)
        {
            _subCategoryServiceDAL = subCategoryServiceDAL;
            _mapper = mapper;
        }

        public async Task<List<SubCategoryDTO>> GetAllAsync()
        {
            var subCategories = await _subCategoryServiceDAL.GetAllAsync();
            return _mapper.Map<List<SubCategoryDTO>>(subCategories);
        }

        public async Task<SubCategoryDTO?> GetByIdAsync(int id)
        {
            var subCategory = await _subCategoryServiceDAL.GetByIdAsync(id);
            return subCategory == null ? null : _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> CreateAsync(SubCategoryDTO subCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            await _subCategoryServiceDAL.AddAsync(subCategory);
            return _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<bool> UpdateAsync(int id, SubCategoryDTO subCategoryDto)
        {
            var subCategory = await _subCategoryServiceDAL.GetByIdAsync(id);
            if (subCategory == null) return false;
            _mapper.Map(subCategoryDto, subCategory);
            await _subCategoryServiceDAL.UpdateAsync(subCategory);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subCategory = await _subCategoryServiceDAL.GetByIdAsync(id);
            if (subCategory == null) return false;
            await _subCategoryServiceDAL.DeleteAsync(subCategory);
            return true;
        }
    }
}
