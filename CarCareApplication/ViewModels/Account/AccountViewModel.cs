using CarCareApplication.Models;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class AccountViewModel : BaseViewModel
    {
        public ICommand NavigationCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand BittechsNavigation { get; set; }
        public ICommand LanguageChangeCommand { get; set; }

        public ObservableRangeCollection<string> Languages { get; set; } = new ObservableRangeCollection<string> { Language.English, Language.Arabic };

        public ObservableRangeCollection<AccountItemGroup> AccountItems { get; set; } = new ObservableRangeCollection<AccountItemGroup>();

        public AccountViewModel()
        {
            LoadCommand = new Command(() =>
            {
                AccountItems.Add(new AccountItemGroup(Language.Account, new List<AccountItem>
                {
                        new AccountItem { Title = Language.Login , Icon = Resources.Fonts.IconFont.Login , Route = "SignIn" , IsVisible = !GlobalResources.Current.ApplicationUser.IsUserLoggedIn},
                        new AccountItem { Title = Language.Signup , Icon = Resources.Fonts.IconFont.AccountPlus , Route = "SignUp" , IsVisible = !GlobalResources.Current.ApplicationUser.IsUserLoggedIn},
                        new AccountItem { Title = Language.Logout , Icon = Resources.Fonts.IconFont.Logout , Route = "Logout" , IsVisible = GlobalResources.Current.ApplicationUser.IsUserLoggedIn},
                        new AccountItem { Title = Language.AppLanguage , Icon = Resources.Fonts.IconFont.Translate , Route = "AppLanguage" , IsVisible = true}
                }.Where(a => a.IsVisible).ToList()));

                AccountItems.Add(new AccountItemGroup(Language.ContactUs, new List<AccountItem>
                {
                    new AccountItem { Title = Language.DialUp , Icon = Resources.Fonts.IconFont.Phone , Route = "DialUp"},
                    new AccountItem { Title = Language.WhatsApp , Icon = Resources.Fonts.IconFont.Whatsapp , Route = "WhatsApp"},
                    new AccountItem { Title = Language.AboutUs , Icon =Resources.Fonts.IconFont.Information , Route = "About"}
                }));
            });

            NavigationCommand = new Command(async (navigationRoute) =>
            {
                string Route = navigationRoute.ToString();
                switch (Route)
                {
                    case "SignIn":
                        await Shell.Current.GoToAsync(nameof(LoginPage));
                        break;
                    case "SignUp":
                        await Shell.Current.GoToAsync(nameof(SignupPage));
                        break;
                    case "Logout":
                        Preferences.Remove(App.UserSharedName);
                        GlobalResources.Current.ApplicationUser.IsUserLoggedIn = false;
                        GlobalResources.Current.ApplicationUser.UserType = UserType.None;
                        Application.Current.MainPage = new AppShell();
                        break;
                    case "Address":
                        await Shell.Current.GoToAsync(nameof(AddressPage));
                        break;
                    case "AppLanguage":
                        await Shell.Current.GoToAsync(nameof(LanguagePage));
                        break;
                    case "DialUp":
                        PhoneDialer.Open("01557667949");
                        break;
                    case "WhatsApp":
                        await Browser.OpenAsync("https://wa.me/+201557667949");
                        break;
                    case "About":
                        await Shell.Current.GoToAsync(nameof(AboutPage));
                        break;
                }
            });

            BittechsNavigation = new Command(async () =>
            {
                await Browser.OpenAsync("https://terratechs.co", BrowserLaunchMode.SystemPreferred);
            });

            LanguageChangeCommand = new Command<string>(async (lang) =>
            {
                if (lang.Equals("ar"))
                    Preferences.Set("AppLang", "ar-EG");
                else
                    Preferences.Set("AppLang", "en-US");

                await App.Current.MainPage.DisplayAlert(Language.Status, Language.RestartRequired, Language.OK);
            });


            LoadCommand.Execute(null);
        }
    }
}