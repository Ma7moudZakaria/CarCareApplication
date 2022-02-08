using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand BittechsNavigation { get; set; }

        public ICommand MakeAppoinmentCommand { get; set; }

        public HomeViewModel()
        {
            MakeAppoinmentCommand = new Command(async () =>
            {
                if (GlobalResources.Current.ApplicationUser.IsUserLoggedIn)
                    await Shell.Current.GoToAsync(nameof(VehiclePage));
                else
                    await App.Current.MainPage.DisplayAlert(Language.Status, Language.LoginRequired, Language.OK);
            });
            BittechsNavigation = new Command(async() =>
            {
                await Browser.OpenAsync("https://terratechs.co", BrowserLaunchMode.SystemPreferred);
            });
        }
    }
}
