using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCareApplication.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDonePage : ContentPage
    {
        public OrderDonePage()
        {
            InitializeComponent();
        }

        private async void GoHome_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }
    }
}