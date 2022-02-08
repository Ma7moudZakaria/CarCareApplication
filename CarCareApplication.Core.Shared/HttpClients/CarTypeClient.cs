using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class CarTypeClient
    {
        private readonly HttpClient _client;
        public CarTypeClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<IEnumerable<IndexCarTypeViewModel>>> GetAsync(string langCode,bool isMobile)
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<IndexCarTypeViewModel>>>($"api/cartype/{langCode}/{isMobile}");
        }

        public async Task<CommitResult<UpdateCarTypeViewModel>> GetCarTypeAsync(int Id)
        {
            return await _client.GetFromJsonAsync<CommitResult<UpdateCarTypeViewModel>>($"api/cartype/{Id}");
        }

        public async Task<CommitResult> CreateAsync(CreateCarTypeViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/cartype", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
        public async Task<CommitResult> UpdateAsync(UpdateCarTypeViewModel model)
        {
            HttpResponseMessage Result = await _client.PutAsJsonAsync("api/cartype", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
        public async Task<CommitResult> ToggleEnableAsnyc(int Id)
        {
            HttpResponseMessage Result = await _client.GetAsync($"api/cartype/toggle/{Id}");
            return await Result.Content.ReadFromJsonAsync<CommitResult>();
        }
    }
}
