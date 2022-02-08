using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Setting
{
    public partial class IndexComponent
    {
        [Inject] public SettingClient Client { get; set; }

        public List<IndexSettingViewModel> ItemsSource { get; set; } = new List<IndexSettingViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexSettingViewModel>> commitResult = await Client.GetAsync(Program.AppLang);
            if (commitResult.IsSuccess)
            {
                ItemsSource.AddRange(commitResult.Value);
            }
            else
            {

            }
        }
    }
}
