using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using CarCareApplication.WebApp.Client.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Account
{
    public partial class RegisterComponent
    {
        [Inject] UserClient Client { get; set; }
        [Inject] AuthenticationStateProvider AuthProvider { get; set; }
        [Inject] IToastService ToastService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        public SignupUserViewModel ViewModel { get; set; } = new SignupUserViewModel();
        public List<RoleViewModel> Roles { get; set; } = new List<RoleViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<RoleViewModel>> commitResult = await Client.GetAllRolesAsync();
            if (commitResult.IsSuccess)
            {
                Roles.AddRange(commitResult.Value);
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }

        public async Task OnValidSubmitAsync()
        {
            CommitResult<TokenResult> commitResult = await Client.SignupAsync(ViewModel);

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
