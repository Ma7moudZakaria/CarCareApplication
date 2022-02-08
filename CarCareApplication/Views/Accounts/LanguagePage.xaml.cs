using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCareApplication.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagePage : ContentPage
    {
        public LanguagePage()
        {
            InitializeComponent();
        }

        private async void BitTech_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://bittechs.tech");
        }
    }
}