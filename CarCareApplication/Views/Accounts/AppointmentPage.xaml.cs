using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCareApplication.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentPage : ContentPage
    {
        public AppointmentPage()
        {
            InitializeComponent();
        }

        private async void BookingAppointment_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int hourofWork = (int)button.CommandParameter;
        }
    }
}