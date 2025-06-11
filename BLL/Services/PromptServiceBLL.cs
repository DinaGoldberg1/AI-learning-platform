using AutoMapper;
using BLL.API;
using BLL.DTOs;
using DAL.API;
using DAL.Models;

namespace BLL.Services
{
    public class PromptServiceBLL : IPromptServiceBLL
    {
        private readonly IPromptServiceDAL _promptServiceDAL;
        private readonly IMapper _mapper;

        public PromptServiceBLL(IPromptServiceDAL promptServiceDAL, IMapper mapper)
        {
            _promptServiceDAL = promptServiceDAL;
            _mapper = mapper;
        }

        public async Task<List<PromptDTO>> GetAllAsync()
        {
            var prompts = await _promptServiceDAL.GetAllAsync();
            return _mapper.Map<List<PromptDTO>>(prompts);
        }

        public async Task<PromptDTO?> GetByIdAsync(int id)
        {
            var prompt = await _promptServiceDAL.GetByIdAsync(id);
            return prompt == null ? null : _mapper.Map<PromptDTO>(prompt);
        }

        public async Task<PromptDTO> CreateAsync(PromptDTO promptDto)
        {
            var prompt = _mapper.Map<Prompt>(promptDto);
            await _promptServiceDAL.AddAsync(prompt);
            return _mapper.Map<PromptDTO>(prompt);
        }
        public async Task<PromptDTO> GenerateLesson(PromptDTO promptDto)
        {
            //      var Response = await _aiService.GenerateLesson(promptDto.Prompt);
            var aiResponse = "";
            var prompt = new Prompt
            {
                UserId = promptDto.UserId,
                CategoryId = promptDto.CategoryId,
                SubCategoryId = promptDto.SubCategoryId,
                PromptText = promptDto.PromptText,
                Response = aiResponse,
                CreatedAt = DateTime.UtcNow
            };

            await _promptServiceDAL.AddAsync(prompt);
            return new PromptDTO { Response = aiResponse };
        }
        public async Task<bool> UpdateAsync(int id, PromptDTO promptDto)
        {
            var prompt = await _promptServiceDAL.GetByIdAsync(id);
            if (prompt == null) return false;
            _mapper.Map(promptDto, prompt);
            await _promptServiceDAL.UpdateAsync(prompt);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prompt = await _promptServiceDAL.GetByIdAsync(id);
            if (prompt == null) return false;
            await _promptServiceDAL.DeleteAsync(prompt);
            return true;
        }
    }
}
