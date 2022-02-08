﻿using Blazored.Toast.Services;
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
    public partial class AddComponent
    {
        public CreateSettingViewModel ViewModel { get; set; } = new CreateSettingViewModel();
        public List<IndexServiceViewModel> ServicesSouce { get; set; } = new List<IndexServiceViewModel>();
        public List<IndexCarTypeViewModel> CarTypeSouce { get; set; } = new List<IndexCarTypeViewModel>();

        [Inject] public SettingClient Client { get; set; }
        [Inject] public ServiceClient ServiceClient { get; set; }
        [Inject] public CarTypeClient CarTypeClient { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public string SelectedService { get; set; }
        public string SelectedCarType { get; set; }

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexCarTypeViewModel>> cartypeCommitResult = await CarTypeClient.GetAsync(Program.AppLang,false);
            if (cartypeCommitResult.IsSuccess)
            {
                CommitResult<IEnumerable<IndexServiceViewModel>> serviceCommitResult = await ServiceClient.GetAsync(Program.AppLang,false);
                if (serviceCommitResult.IsSuccess)
                {
                    CarTypeSouce.AddRange(cartypeCommitResult.Value);
                    ServicesSouce.AddRange(serviceCommitResult.Value);
                    SelectedService = ServicesSouce.FirstOrDefault().Id.ToString();
                    SelectedCarType = CarTypeSouce.FirstOrDefault().Id.ToString();
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

        public async Task OnValidSubmitAsync()
        {
            if (ViewModel.KilometerRate > 0)
            {
                ViewModel.ServiceId = int.Parse(SelectedService);
                ViewModel.CarTypeId = int.Parse(SelectedCarType);
                if (ViewModel.KilometerMin < ViewModel.KilometerMax)
                {
                    CommitResult commitResult = await Client.CreateAsync(ViewModel);
                    if (commitResult.IsSuccess)
                    {
                        ToastService.ShowSuccess(string.Empty, Loc["SettingSuccess"]);
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
