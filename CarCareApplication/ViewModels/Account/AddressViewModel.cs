using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.AddressModels;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class AddressViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddressSelectedCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ObservableRangeCollection<IndexAddressViewModel> Addresses { get; set; } = new ObservableRangeCollection<IndexAddressViewModel>();


        private IndexAddressViewModel _selectedAddress;

        public IndexAddressViewModel SelectedAddress
        {
            get { return _selectedAddress; }
            set { _selectedAddress = value; OnPropertyChanged(nameof(SelectedAddress)); }
        }

        public AddressClient Client { get; set; }
        public SettingClient SettingClient { get; set; }

        public AddressViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                IsRunning = true;
                Addresses.Clear();
                Client = new AddressClient(App.HttpClient);
                SettingClient = new SettingClient(App.HttpClient);
                CommitResult<IEnumerable<IndexAddressViewModel>> commitResult = await Client.GetAsync(GlobalResources.Current.ApplicationUser.Id);
                if (commitResult.IsSuccess)
                {
                    Addresses.AddRange(commitResult.Value);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                IsRunning = false;
            });

            AddressSelectedCommand = new Command(async () =>
            {
                if (Request.Current.HourOfWorkId == 0 || Request.Current.CarTypeId == 0 || Request.Current.ServiceId == 0) return;

                if (SelectedAddress is null) return;

                Request.Current.AddressId = SelectedAddress.Id;
                Request.Current.Address = SelectedAddress.FullAddress;

                CommitResult<float> commitResult = await SettingClient.GetIfServiceAvaliableAsync(new CheckAreaSupportViewModel
                {
                    CarTypeId = Request.Current.CarTypeId,
                    ServiceId = Request.Current.ServiceId,
                    KilometerAway = SelectedAddress.KilometerAway
                });

                if (commitResult.IsSuccess)
                {
                    Request.Current.DeliveryPrice = (float)Math.Round(commitResult.Value);
                    await Shell.Current.GoToAsync(nameof(PlaceOrderPage));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                SelectedAddress = null;
            });
            AddCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AddAddressPage));
            });

            RemoveCommand = new Command<int>(async (Id) =>
            {
                CommitResult commitResult = await Client.DeleteAsync(Id);
                if (commitResult.IsSuccess)
                {
                    var tempAddress = Addresses.SingleOrDefault(a => a.Id.Equals(Id));
                    Addresses.Remove(tempAddress);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
            });
        }
    }
}
