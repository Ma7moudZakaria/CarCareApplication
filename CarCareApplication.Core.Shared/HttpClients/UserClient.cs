using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.HttpClients
{
    public class UserClient
    {
        private readonly HttpClient _client;
        public UserClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitResult<TokenResult>> SigninAsync(SigninUserViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/account/signin", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult<TokenResult>>();
        }

        public async Task<CommitResult<TokenResult>> SignupAsync(SignupUserViewModel model)
        {
            HttpResponseMessage Result = await _client.PostAsJsonAsync("api/account/signup", model);
            return await Result.Content.ReadFromJsonAsync<CommitResult<TokenResult>>();
        }

        public async Task<CommitResult<IEnumerable<RoleViewModel>>> GetAllRolesAsync()
        {
            return await _client.GetFromJsonAsync<CommitResult<IEnumerable<RoleViewModel>>>("api/account/get-all-roles");
        }
    }
}
