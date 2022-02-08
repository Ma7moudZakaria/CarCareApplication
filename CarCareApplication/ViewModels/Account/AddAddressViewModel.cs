using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.AddressModels;
using CarCareApplication.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CarCareApplication.ViewModels.Account
{
    public class AddAddressViewModel : BaseViewModel
    {
        public Xamarin.Forms.Maps.Map AppMap { get; set; }

        public ICommand AddAddressCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        public Location CarLocation { get; set; } = new Location { Latitude = 29.97465, Longitude = 31.3153553 };

        public Location UserLocation { get; set; }

        public Position UserPosition { get; internal set; }


        private CreateAddressViewModel _address = new CreateAddressViewModel();

        public CreateAddressViewModel Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public AddressClient Client { get; set; }
        public SettingClient SettingClient { get; set; }
        private void LoadLocation()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    var cts = new CancellationTokenSource();
                    UserLocation = await Geolocation.GetLocationAsync(request, cts.Token);
                    UserPosition = new Position(UserLocation.Latitude, UserLocation.Longitude);
                    if (!UserLocation.IsFromMockProvider)
                    {
                        AppMap.MoveToRegion(MapSpan.FromCenterAndRadius(UserPosition, Distance.FromMeters(100)));
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await App.Current.MainPage.DisplayAlert(Language.Status, Language.GPSLocationError, Language.OK);
                    await Shell.Current.GoToAsync("..");
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    await App.Current.MainPage.DisplayAlert(Language.Status, Language.GPSDeciveError, Language.OK);
                    await Shell.Current.GoToAsync("..");
                }
                catch (PermissionException pEx)
                {
                    await App.Current.MainPage.DisplayAlert(Language.Status, Language.LocationPermissionError, Language.OK);
                    await Shell.Current.GoToAsync("..");
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert(Language.Status, Language.LocationError, Language.OK);
                    await Shell.Current.GoToAsync("..");
                    // Unable to get location
                }
            });
        }
        public AddAddressViewModel()
        {
            LoadCommand = new Command(() =>
            {
                IsRunning = true;
                LoadLocation();
                IsRunning = false;
            });
            AddAddressCommand = new Command(async () =>
            {
                if (UserLocation == default)
                    LoadLocation();

                if (string.IsNullOrWhiteSpace(Address.Name) || string.IsNullOrWhiteSpace(Address.FullAddress) || string.IsNullOrWhiteSpace(Address.PhoneNumber) || string.IsNullOrWhiteSpace(Address.Type)) return;


                UserLocation.Latitude = UserPosition.Latitude;
                UserLocation.Longitude = UserPosition.Longitude;

                Address.KilometersAway = (float)Math.Round(Location.CalculateDistance(CarLocation, UserLocation, DistanceUnits.Kilometers));
                Address.Latitude = UserPosition.Latitude;
                Address.Longitude = UserPosition.Longitude;
                Address.UserId = GlobalResources.Current.ApplicationUser.Id;

                SettingClient = new SettingClient(App.HttpClient);

                CommitResult<float> commitResult = await SettingClient.GetIfServiceAvaliableAsync(new Core.Shared.ViewModels.SettingModels.CheckAreaSupportViewModel
                {
                    CarTypeId = Request.Current.CarTypeId,
                    ServiceId = Request.Current.ServiceId,
                    KilometerAway = Address.KilometersAway
                });

                if (commitResult.IsSuccess)
                {
                    Client = new AddressClient(App.HttpClient);
                    Request.Current.DeliveryPrice = commitResult.Value;
                    CommitResult addressResult = await Client.CreateAsync(Address);

                    if (addressResult.IsSuccess)
                    {
                        await Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(addressResult.ErrorCode), Language.OK);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
            });
        }
    }
}
