using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.ExtraPriceSetting
{
    public partial class AddComponent
    {
        public string CarTypeId { get; set; }
        public string ServiceId { get; set; }

        public CreateExtraPriceSettingViewModel ViewModel { get; set; } = new CreateExtraPriceSettingViewModel();
        public List<IndexCarTypeViewModel> CarTypes { get; set; } = new List<IndexCarTypeViewModel>();
        public List<IndexServiceViewModel> Services { get; set; } = new List<IndexServiceViewModel>();

        [Inject] public CarTypeClient CarTypeClient { get; set; }
        [Inject] public ServiceClient ServiceClient { get; set; }
        [Inject] public ExtraPriceSettingClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexCarTypeViewModel>> cartypeCommitResult = await CarTypeClient.GetAsync(Program.AppLang,false);
            if (cartypeCommitResult.IsSuccess)
            {
                CommitResult<IEnumerable<IndexServiceViewModel>> serviceCommitResult = await ServiceClient.GetAsync(Program.AppLang,false);
                if (serviceCommitResult.IsSuccess)
                {
                    CarTypes.AddRange(cartypeCommitResult.Value);
                    Services.AddRange(serviceCommitResult.Value);
                    ServiceId = CarTypes.FirstOrDefault().Id.ToString();
                    CarTypeId = CarTypes.FirstOrDefault().Id.ToString();
                }
                else
                {
                    ToastService.ShowError(serviceCommitResult.ErrorType.ToString(), Loc[serviceCommitResult.ErrorCode]);
                }
            }
            else
            {
                ToastService.ShowError(cartypeCommitResult.ErrorType.ToString(), Loc[cartypeCommitResult.ErrorCode]);
            }
        }

        public async Task OnValidSubmit()
        {
            ViewModel.CarTypeId = int.Parse(CarTypeId);
            ViewModel.ServiceId = int.Parse(ServiceId);
            CommitResult commitResult = await Client.CreateAsync(ViewModel);
            if (commitResult.IsSuccess)
            {
                ToastService.ShowSuccess(string.Empty, Loc["ExtraPriceSettingSuccess"]);
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
    }
}
