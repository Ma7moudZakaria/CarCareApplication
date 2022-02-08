using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.RevenueModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Revenues
{
    public partial class AddComponent
    {
        public CreateRevenueViewModel ViewModel { get; set; } = new CreateRevenueViewModel();

        [Inject] public RevenueClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public async Task OnValidSubmit()
        {
            CommitResult commitResult = await Client.CreateAsync(ViewModel);

            if (commitResult.IsSuccess)
            {
                ToastService.ShowSuccess(string.Empty, Loc["RevenueSuccess"]);
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
    }
}
