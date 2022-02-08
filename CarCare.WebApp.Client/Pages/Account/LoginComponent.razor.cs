using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using CarCareApplication.WebApp.Client.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Account
{
    public partial class LoginComponent
    {
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] UserClient Client { get; set; }
        [Inject] AuthenticationStateProvider AuthProvider { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public SigninUserViewModel ViewModel { get; set; } = new SigninUserViewModel();

        public async Task OnValidSubmitAsync()
        {
            CommitResult<TokenResult> commitResult = await Client.SigninAsync(ViewModel);

            if (commitResult.IsSuccess)
            {
                await ((ApiAuthenticationStateProvider)AuthProvider).MarkUserAsAuthenticatedAsync(commitResult.Value);
                Navigation.NavigateTo("/");
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
    }
}
