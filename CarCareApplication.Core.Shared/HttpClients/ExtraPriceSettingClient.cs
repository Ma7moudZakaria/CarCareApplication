using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class ExtraPriceSettingClient
    {
        private readonly HttpClient _client;
        public ExtraPriceSettingClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>> GetAsync(string langCode)
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>>($"api/extraPriceSetting/{langCode}");
        }

        public async Task<CommitResult<UpdateExtraPriceSettingViewModel>> GetItem(int Id)
        {
            return await _client.GetFromJsonAsync<CommitResult<UpdateExtraPriceSettingViewModel>>($"api/extraPriceSetting/{Id}");
        }

        public async Task<CommitResult> CreateAsync(CreateExtraPriceSettingViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/extraPriceSetting", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> UpdateAsync(UpdateExtraPriceSettingViewModel model)
        {
            HttpResponseMessage Result = await _client.PutAsJsonAsync("api/extraPriceSetting", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
