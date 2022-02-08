using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.ExpensesModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class ExpenseClient
    {
        private readonly HttpClient _client;
        public ExpenseClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexExpenseViewModel>>> GetAsync()
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexExpenseViewModel>>>($"api/expense");
        }

        public async Task<CommitResult> CreateAsync(CreateExpenseViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/expense", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult<float>> GetRevenueCashAsync()
        {
            return await _client.GetFromJsonAsync<CommitResult<float>>($"api/expense/GetCash");
        }
    }
}
