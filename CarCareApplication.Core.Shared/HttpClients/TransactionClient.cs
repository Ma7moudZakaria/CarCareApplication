using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class TransactionClient
    {
        private readonly HttpClient _client;
        public TransactionClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>> GetAsync(string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>>($"api/Transaction/{langCode}");
        }
        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>> GetAsync(int userId, string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>>($"api/Transaction/{userId}/{langCode}");
        }
        public async Task<CommitResult<TransactionDetailViewModel>> GetDetailAsync(int Id, string langCode = "ar")
        {
            return await _client.GetFromJsonAsync<CommitResult<TransactionDetailViewModel>>($"api/Transaction/Detail/{Id}/{langCode}");
        }
        public async Task<CommitResult> CreateAsync(CreateTransactionViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/Transaction", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> DoneAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.PutAsync($"api/Transaction/{Id}", null);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> DeleteAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.DeleteAsync($"api/Transaction/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
