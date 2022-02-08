using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using CarCareApplication.Resources;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CarCareApplication.ViewModels.Orders
{
    [QueryProperty("OrderId", "Id")]
    public class AdminOrderDetailViewModel : BaseViewModel
    {
        public Map AppMap { get; set; }

        private int _orderId;
        public int OrderId
        {
            get { return _orderId; }
            set
            {
                _orderId = int.Parse(Uri.UnescapeDataString(value.ToString()));
                LoadCommand.Execute(null);
            }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand CallCommand { get; set; }
        public ICommand OpenGoogleMapsCommand { get; set; }

        private TransactionDetailViewModel _adminOrderDetail;

        public TransactionDetailViewModel AdminOrderDetail
        {
            get { return _adminOrderDetail; }
            set { _adminOrderDetail = value; OnPropertyChanged(nameof(AdminOrderDetail)); }
        }

        public TransactionClient Client { get; set; }
        public AdminOrderDetailViewModel()
        {
            CallCommand = new Command<string>((number) =>
           {
               Xamarin.Essentials.PhoneDialer.Open(number);
           });
            LoadCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsRunning = true;

                    Client = new TransactionClient(App.HttpClient);

                    CommitResult<TransactionDetailViewModel> commitResult = await Client.GetDetailAsync(OrderId, App.LangCode);

                    if (commitResult.IsSuccess)
                    {
                        AdminOrderDetail = commitResult.Value;
                        Position position = new Position(AdminOrderDetail.Latitude, AdminOrderDetail.Longitude);

                        AppMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(100)));

                        AppMap.Pins.Add(new Pin
                        {
                            Label = Language.RequestLocation,
                            Address = AdminOrderDetail.FullAddress,
                            Type = PinType.Generic,
                            Position = position
                        });
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                    }

                    IsRunning = false;

                });
            });

            OpenGoogleMapsCommand = new Command(async () =>
            {
                await Xamarin.Essentials.Map.OpenAsync(new Xamarin.Essentials.Location(AdminOrderDetail.Latitude, AdminOrderDetail.Longitude), new Xamarin.Essentials.MapLaunchOptions
                {
                    Name = Language.GoTo,
                    NavigationMode = Xamarin.Essentials.NavigationMode.Driving
                });
            });
        }
    }
}
