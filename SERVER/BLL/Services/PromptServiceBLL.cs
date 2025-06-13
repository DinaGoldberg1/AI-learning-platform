using AutoMapper;
using BLL.API;
using BLL.DTOs;
using DAL.API;
using DAL.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

        public async Task<string> ProcessPromptAsync(PromptDTO promptDto)
        {
            var prompt=_mapper.Map<Prompt>(promptDto);
            var response = await SendPromptToAI(promptDto.PromptText);
            prompt.Response = response;

            await _promptServiceDAL.AddAsync(prompt); 
            return response;
        }

        public async Task<List<PromptDTO>> GetUserHistoryAsync(int userId)
        {
            var prompts = await _promptServiceDAL.GetUserHistoryAsync(userId);
            return prompts.Select(p => new PromptDTO
            {
                PromptText = p.PromptText,
                Response = p.Response,
                CreatedAt = p.CreatedAt
            }).ToList();
        }


        private async Task<string> SendPromptToAI(string promptText)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

                var payload = new
                {
                    prompt = promptText,
                    max_tokens = 150
                };

                var response = await client.PostAsJsonAsync("https://api.openai.com/v1/engines/davinci/completions", payload);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic jsonResult = JsonConvert.DeserializeObject(jsonResponse);

                return jsonResult.choices[0].text.ToString();
            }
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
