using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.CarType
{
    public partial class EditComponent
    {
        public UpdateCarTypeViewModel ViewModel { get; set; } = new UpdateCarTypeViewModel();
        [Inject] public CarTypeClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }


        [Parameter] public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CommitResult<UpdateCarTypeViewModel> commitResult = await Client.GetCarTypeAsync(Id);
            if (commitResult.IsSuccess)
            {
                ViewModel = commitResult.Value;
            }
            else
            {

            }
        }
        public async Task OnValidSubmitAsync()
        {
            CommitResult commitResult = await Client.UpdateAsync(ViewModel);
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
