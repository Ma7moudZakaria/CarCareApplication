using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.CarType
{
    public partial class AddComponent
    {
        public CreateCarTypeViewModel ViewModel { get; set; } = new CreateCarTypeViewModel();
        [Inject] public CarTypeClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public async Task OnValidSubmitAsync()
        {
            CommitResult commitResult = await Client.CreateAsync(ViewModel);
            if (commitResult.IsSuccess)
            {
                ToastService.ShowSuccess(string.Empty, Loc["CarTypeSuccess"]);
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
    }
}
