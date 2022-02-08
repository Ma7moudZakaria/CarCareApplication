using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.ExtraPriceSetting
{
    public partial class IndexComponent
    {
        [Inject] public ExtraPriceSettingClient Client { get; set; }

        public List<IndexExtraPriceSettingViewModel> ItemsSouce { get; set; } = new List<IndexExtraPriceSettingViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>> commitResult = await Client.GetAsync(Program.AppLang);
            if (commitResult.IsSuccess)
            {
                ItemsSouce.AddRange(commitResult.Value);
            }
            else
            {

            }
        }
    }
}
