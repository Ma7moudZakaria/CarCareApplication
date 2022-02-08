using CarCareApplication.ViewModels.Account;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CarCareApplication.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressPage : ContentPage
    {
        AddAddressViewModel addAddressViewModel;
        public AddAddressPage()
        {
            addAddressViewModel = new AddAddressViewModel();
            InitializeComponent();
            this.BindingContext = addAddressViewModel;
            addAddressViewModel.AppMap = map;
        }

        private void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            addAddressViewModel.UserPosition = e.Position;

            map.Pins.Clear();

            Pin pin = new Pin
            {
                Label = "Location",
                Address = addAddressViewModel.Address.FullAddress,
                Type = PinType.Generic,
                Position = e.Position
            };
            map.Pins.Add(pin);
        }
    }
}