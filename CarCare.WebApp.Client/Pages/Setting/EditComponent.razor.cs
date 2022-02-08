using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Setting
{
    public partial class EditComponent
    {
        public UpdateSettingViewModel ViewModel { get; set; } = new UpdateSettingViewModel();
        [Inject] IToastService ToastService { get; set; }
        public List<IndexServiceViewModel> ServicesSouce { get; set; } = new List<IndexServiceViewModel>();
        public List<IndexCarTypeViewModel> CarTypeSouce { get; set; } = new List<IndexCarTypeViewModel>();

        [Inject] public ServiceClient ServiceClient { get; set; }
        [Inject] public SettingClient Client { get; set; }
        [Inject] public CarTypeClient CarTypeClient { get; set; }


        [Parameter] public int Id { get; set; }
        public string SelectedService { get; set; }
        public string SelectedCarType { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CommitResult<UpdateSettingViewModel> commitResult  = await Client.GetSettingAsync(Id);
            if (commitResult.IsSuccess)
            {
                CommitResult<IEnumerable<IndexCarTypeViewModel>> cartypeCommitResult = await CarTypeClient.GetAsync(Program.AppLang,false);
                if (cartypeCommitResult.IsSuccess)
                {
                    CommitResult<IEnumerable<IndexServiceViewModel>> serviceCommitResult = await ServiceClient.GetAsync(Program.AppLang,false);
                    if (serviceCommitResult.IsSuccess)
                    {
                        ViewModel = commitResult.Value;
                        CarTypeSouce.AddRange(cartypeCommitResult.Value);
                        ServicesSouce.AddRange(serviceCommitResult.Value);
                        SelectedService = ViewModel.ServiceId.ToString();
                        SelectedCarType = ViewModel.CarTypeId.ToString();
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
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
        public async Task OnValidSubmitAsync()
        {
            if (ViewModel.KilometerRate > 0)
            {
                ViewModel.ServiceId = int.Parse(SelectedService);
                ViewModel.CarTypeId = int.Parse(SelectedCarType);
                if (ViewModel.KilometerMin < ViewModel.KilometerMax)
                {
                    CommitResult commitResult = await Client.UpdateAsync(ViewModel);
                    if (commitResult.IsSuccess)
                    {
                        ToastService.ShowSuccess(string.Empty, "Loc[SettingSuccess]");
                    }
                    else
                    {
                        ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
                    }
                }
                else
                    ToastService.ShowError(string.Empty, Loc["KilometerMin"]);
            }
            else
                ToastService.ShowError(string.Empty, Loc["KilometerRate"]);
        }
    }
}
