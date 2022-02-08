using CarCareApplication.Models;
using System.Text.Json;
using Xamarin.Essentials;

namespace CarCareApplication.Resources
{
    public class GlobalResources : BindableBase
    {
        // Singleton
        public static GlobalResources Current = new GlobalResources();

        public GlobalResources()
        {
            if (Preferences.ContainsKey(App.UserSharedName))
            {
                string user = Preferences.Get(App.UserSharedName, null);
                ApplicationUser = JsonSerializer.Deserialize<ApplicationUser>(user);
            }
            else
            {
                ApplicationUser = new ApplicationUser()
                {
                    IsUserLoggedIn = false,
                    UserType = UserType.None
                };
            }
        }

        private ApplicationUser _appplicationUser;

        public ApplicationUser ApplicationUser
        {
            get { return _appplicationUser; }
            set
            {
                _appplicationUser = value; OnPropertyChanged(nameof(ApplicationUser));
            }
        }
    }
}
