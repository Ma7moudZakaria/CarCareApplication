using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using CarCareApplication.Core.Shared.ViewModels.SharedModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class ServiceClient
    {
        private readonly HttpClient _client;
        public ServiceClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitResult<IEnumerable<IndexServiceViewModel>>> GetAsync(string langCode, bool isMobile)
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexServiceViewModel>>>($"api/service/{langCode}/{isMobile}");
        }
        public async Task<CommitResult<IEnumerable<IndexServiceViewModel>>> GetAsync(string langCode, int CarType)
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexServiceViewModel>>>($"api/service/GetServicesForCarType/{langCode}/{CarType}");
        }

        public async Task<CommitResult> UpdateAsync(UpdateServiceViewModel model)
        {
            HttpResponseMessage Result = await _client.PutAsJsonAsync("api/service", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult<UpdateServiceViewModel>> GetServiceAsync(int id)
        {
            return await _client.GetFromJsonAsync<CommitResult<UpdateServiceViewModel>>($"api/service/{id}");
        }

        public async Task<CommitResult> CreateAsync(CreateServiceViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/service", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }

        public async Task<CommitResult> ToggleEnableAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.GetAsync($"api/service/toggle/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
