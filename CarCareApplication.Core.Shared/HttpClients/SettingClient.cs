using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class SettingClient
    {
        private readonly HttpClient _client;
        public SettingClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexSettingViewModel>>> GetAsync(string langCode)
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexSettingViewModel>>>($"api/setting/{langCode}");
        }

        public async Task<CommitResult> CreateAsync(CreateSettingViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/setting", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
        public async Task<CommitResult> UpdateAsync(UpdateSettingViewModel model)
        {
            HttpResponseMessage Result = await _client.PutAsJsonAsync("api/setting", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
        public async Task<CommitResult<UpdateSettingViewModel>> GetSettingAsync(int Id)
        {
            return await _client.GetFromJsonAsync<CommitResult<UpdateSettingViewModel>>($"api/setting/{Id}");
        }

        public async Task<CommitResult<float>> GetIfServiceAvaliableAsync(CheckAreaSupportViewModel viewModel)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync($"api/setting/CheckAreaSupport", viewModel);
            return await Result.Content.ReadFromJsonAsync<CommitResult<float>>();
        }
    }
}
