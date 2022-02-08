using Blazored.Toast.Services;
using CarCareApplication.WebApp.Client.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Account
{
    public partial class LogoutComponent
    {
        [Inject] AuthenticationStateProvider AuthProvider { get; set; }
        [Inject] IToastService ToastService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ((ApiAuthenticationStateProvider)AuthProvider).MarkUserAsLoggedOutAsync();
            ToastService.ShowInfo("You have been logged out!");
        }
    }
}
