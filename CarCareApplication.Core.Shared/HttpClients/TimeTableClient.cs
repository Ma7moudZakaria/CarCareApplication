using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class TimeTableClient
    {
        private readonly HttpClient _client;
        public TimeTableClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>> GetAsync(string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>>($"api/hourOfWork/{langCode}");
        }
        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>> GetAsync(int ServiceId, string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>>($"api/hourOfWork/{langCode}/{ServiceId}");
        }

        public async Task<CommitResult> CreateAsync(CreateHourOfWorkViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/hourOfWork", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> ToggleEnableAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.GetAsync($"api/hourOfWork/toggle/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
