using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using CarCareApplication.Models;
using CarCareApplication.Resources;
using System.Text.Json;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class SigninViewModel : BaseViewModel
    {
        public ICommand SigninCommand { get; set; }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        public UserClient Client { get; set; }
        public SigninViewModel()
        {
            SigninCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(PhoneNumber)) return;

                IsRunning = true;

                Client = new UserClient(App.HttpClient);

                CommitResult<TokenResult> commitResult = await Client.SigninAsync(new SigninUserViewModel
                {
                    Password = Password,
                    PhoneNumber = PhoneNumber
                });

                if (commitResult.IsSuccess)
                {
                    if (!Preferences.ContainsKey(App.UserSharedName))
                    {
                        Preferences.Set(App.UserSharedName, JsonSerializer.Serialize(new ApplicationUser
                        {
                            Id = commitResult.Value.UserId,
                            IsUserLoggedIn = true,
                            Token = commitResult.Value.Token,
                            UserType = commitResult.Value.UserType == "User" ? UserType.Customer : commitResult.Value.UserType == "Admin" ? UserType.Admin : UserType.Driver,
                        }));

                        GlobalResources.Current.ApplicationUser = JsonSerializer.Deserialize<ApplicationUser>(Preferences.Get(App.UserSharedName, null));
                        IsRunning = false;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        GlobalResources.Current.ApplicationUser = JsonSerializer.Deserialize<ApplicationUser>(Preferences.Get(App.UserSharedName, null));
                        IsRunning = false;
                    }
                }
                else
                {
                    IsRunning = false;
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
            });
        }
    }
}
