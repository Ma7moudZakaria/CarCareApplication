using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.DayModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class DayClient
    {
        private readonly HttpClient _client;
        public DayClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexDayViewModel>>> GetAsync(string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexDayViewModel>>>($"api/day/{langCode}");
        }

        public async Task<CommitResult> CreateAsync(CreateDayViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/day", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> ToggleEnableAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.GetAsync($"api/day/toggle/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
