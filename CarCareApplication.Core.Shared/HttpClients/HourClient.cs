using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.HourModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class HourClient
    {
        private readonly HttpClient _client;
        public HourClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexHourViewModel>>> GetAsync(string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexHourViewModel>>>($"api/hour/{langCode}");
        }

        public async Task<CommitResult> CreateAsync(CreateHourViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/hour", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> ToggleEnableAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.GetAsync($"api/hour/toggle/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
