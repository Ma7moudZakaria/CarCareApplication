using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Service
{
    public partial class AddComponent
    {
        public CreateServiceViewModel ViewModel { get; set; } = new CreateServiceViewModel();

        [Inject] public ServiceClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public async Task OnValidSubmitAsync()
        {
            if (ViewModel.BasePrice > 0)
            {
                CommitResult commitResult = await Client.CreateAsync(ViewModel);
                if (commitResult.IsSuccess)
                {
                    ToastService.ShowSuccess(string.Empty, Loc["ServiceSuccess"]);
                }
                else
                {
                    ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
                }
            }
            else
                ToastService.ShowError(string.Empty, Loc["BasePriceError"]);
        }
    }
}
