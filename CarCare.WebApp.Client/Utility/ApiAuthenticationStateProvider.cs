using Blazored.LocalStorage;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Utility
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenResult = await _localStorage.GetItemAsync<TokenResult>("Auth");
            if (tokenResult == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            var handler = new JwtSecurityTokenHandler().ReadJwtToken(tokenResult.Token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(handler.Claims, "jwt")));
        }

        public async Task MarkUserAsAuthenticatedAsync(TokenResult tokenResult)
        {
            await _localStorage.SetItemAsync("Auth", tokenResult);
            var handler = new JwtSecurityTokenHandler().ReadJwtToken(tokenResult.Token);
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(handler.Claims, "jwt"))));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await _localStorage.RemoveItemAsync("Auth");
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
