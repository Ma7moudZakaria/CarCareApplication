using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.HourModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Hours
{
    public partial class IndexComponent
    {
        [Inject] public HourClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public List<IndexHourViewModel> ItemsSource { get; set; } = new List<IndexHourViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexHourViewModel>> commitResult = await Client.GetAsync(Program.AppLang);
            if (commitResult.IsSuccess)
            {
                ItemsSource.AddRange(commitResult.Value);
            }
            else
            {

            }
        }

        private async Task ToggleEnable(int Id)
        {
            CommitResult commitResult = await Client.ToggleEnableAsnyc(Id);
            if (commitResult.IsSuccess)
            {
                ToastService.ShowSuccess("Toggole has been done successfully");
            }
            else
            {
                ToastService.ShowError("Toggole couldn't be done correctly, try again later");
            }
        }
    }
}
