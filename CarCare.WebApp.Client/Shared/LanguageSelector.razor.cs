using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Shared
{
    public partial class LanguageSelector
    {
        public string SelectedLanguage { get; set; }
        [Inject] IToastService ToastService { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        protected override void OnInitialized()
        {
            SelectedLanguage = CultureInfo.DefaultThreadCurrentCulture.Name;
            base.OnInitialized();
        }
        private async Task ChangeLanguage()
        {
            if (SelectedLanguage.Equals("ar-EG"))
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-EG");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar-EG");
                await LocalStorage.SetItemAsync<string>("AppLanguage", "ar-EG");
                ToastService.ShowSuccess("لقد تم تغيير اللغة الى اللغة العربية بنجاح");
            }
            else
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
                await LocalStorage.SetItemAsync<string>("AppLanguage", "en-US");
                ToastService.ShowSuccess("Changing language to English has been done successfully");
            }
            // Refresh the page to load the culture info.
            Navigation.NavigateTo("/", true);
        }
    }


}
