using CarCareApplication.Resources;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class LanguagePageViewModel : BaseViewModel
    {
        public ICommand LangageLanguageCommnad { get; set; }

        public LanguagePageViewModel()
        {
            LangageLanguageCommnad = new Command<string>(async (langCode) =>
            {
                Preferences.Set("AppLang", langCode);
                await App.Current.MainPage.DisplayAlert(Language.Status, Language.RestartRequired, Language.OK);
            });
        }

    }
}
