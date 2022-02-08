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
    public class SignupViewModel : BaseViewModel
    {
        public ICommand SignupCommand { get; set; }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }


        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set { _secondName = value; OnPropertyChanged(nameof(SecondName)); }
        }

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

        public SignupViewModel()
        {
            SignupCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(SecondName) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password)) return;
                IsRunning = true;
                Client = new UserClient(App.HttpClient);

                CommitResult<TokenResult> commitResult = await Client.SignupAsync(new SignupUserViewModel
                {
                    FirstName = FirstName,
                    SecondName = SecondName,
                    Password = Password,
                    PhoneNumber = PhoneNumber,
                    RoleId = 2
                });

                if (commitResult.IsSuccess)
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
                    App.Current.MainPage = new AppShell();
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
