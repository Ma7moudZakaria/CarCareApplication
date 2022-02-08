using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.AddressModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class AddressClient
    {
        private readonly HttpClient _client;
        public AddressClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexAddressViewModel>>> GetAsync(int UserId)
            => await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexAddressViewModel>>>($"api/address/{UserId}");

        public async Task<CommitResult> CreateAsync(CreateAddressViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/address", model);
            return await Result.Content.ReadFromJsonAsync< CommitResult>();
        }

        public async Task<CommitResult> DeleteAsync(int Id)
        {
            HttpResponseMessage Result = await _client.DeleteAsync($"api/address/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

    }
}
