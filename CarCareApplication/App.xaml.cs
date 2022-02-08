using CarCareApplication.Resources;
using System;
using System.Globalization;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCareApplication
{
    public partial class App : Application
    {
        public const string UserSharedName = "Test";

        public const string BaseAddress = "https://CarCareApplication.azurewebsites.net/";

        public static string LangCode = "en";

        public static HttpClient HttpClient { get; set; }

        public App()
        {
            InitializeComponent();

            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress),
                Timeout = TimeSpan.FromSeconds(120)
            };

            if (Preferences.ContainsKey("AppLang"))
            {
                Device.SetFlowDirection(Preferences.Get("AppLang", "ar-EG") == "ar-EG" ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);

                if (Device.FlowDirection == FlowDirection.RightToLeft)
                {
                    CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");
                    CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
                    Language.Culture = new CultureInfo("ar-EG");
                    HttpClient.DefaultRequestHeaders.Add("Accept-Language", "ar-EG");
                    LangCode = "ar";
                }
                else
                {
                    CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");
                    Language.Culture = new CultureInfo("en-US");
                    HttpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US");
                    LangCode = "en";
                }
            }
            else
            {
                Device.SetFlowDirection(FlowDirection.RightToLeft);
                CultureInfo.CurrentUICulture = new CultureInfo("ar-EG");
                CultureInfo.CurrentCulture = new CultureInfo("ar-EG");
                Language.Culture = new CultureInfo("ar-EG");
                HttpClient.DefaultRequestHeaders.Add("Accept-Language", "ar-EG");
                LangCode = "ar";
            }
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
