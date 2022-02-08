using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using CarCareApplication.Views.Orders;
using System.Net.Http.Headers;
using Xamarin.Forms;

namespace CarCareApplication
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public AppShell()
        {
            InitializeComponent();
            if (GlobalResources.Current.ApplicationUser.IsUserLoggedIn)
            {
                App.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalResources.Current.ApplicationUser.Token);
            }
            Routing.RegisterRoute(nameof(AppointmentPage), typeof(AppointmentPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
            Routing.RegisterRoute(nameof(ServicePage), typeof(ServicePage));
            Routing.RegisterRoute(nameof(VehiclePage), typeof(VehiclePage));
            Routing.RegisterRoute(nameof(AddAddressPage), typeof(AddAddressPage));
            Routing.RegisterRoute(nameof(AddressPage), typeof(AddressPage));
            Routing.RegisterRoute(nameof(OrderDonePage), typeof(OrderDonePage));
            Routing.RegisterRoute(nameof(PlaceOrderPage), typeof(PlaceOrderPage));
            Routing.RegisterRoute(nameof(AdminOrderDetailPage), typeof(AdminOrderDetailPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(LanguagePage), typeof(LanguagePage));
        }

    }
}
