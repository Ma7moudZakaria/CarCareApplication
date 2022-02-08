using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.RevenueModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class RevenueClient
    {
        private readonly HttpClient _client;
        public RevenueClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexRevenueViewModel>>> GetAsync()
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexRevenueViewModel>>>($"api/revenue");
        }

        public async Task<CommitResult> CreateAsync(CreateRevenueViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/revenue", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult<float>> GetRevenueCashAsync()
        {
            return await _client.GetFromJsonAsync<CommitResult<float>>($"api/revenue/GetCash");
        }
    }
}
