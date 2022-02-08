using Blazored.LocalStorage;
using Blazored.Toast;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using CarCareApplication.WebApp.Client.Utility;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client
{
    public class Program
    {
        public static string AppLang { get; set; }

        public static void RegisterClient(WebAssemblyHostBuilder builder, HttpClient client)
        {
            var localStorage = builder.Services.BuildServiceProvider().GetService<ISyncLocalStorageService>();
            var tokenResult = localStorage.GetItem<TokenResult>("Auth");
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            if (tokenResult != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.Token);
        }

        public static void InitalizeLocalization(WebAssemblyHostBuilder builder)
        {
            var localStorage = builder.Services.BuildServiceProvider().GetService<ISyncLocalStorageService>();
            var culture = localStorage.GetItem<string>("AppLanguage");
            if (string.IsNullOrWhiteSpace(culture))
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
                AppLang = "en";
            }
            else
            {
                if (culture.Equals("ar-EG"))
                {
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-EG");
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ar-EG");
                    AppLang = "ar";
                }
                else
                {
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
                    AppLang = "en";
                }
            }
        }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            builder.Services.AddHttpClient<CarTypeClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<ServiceClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<ExpenseClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<RevenueClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<SettingClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<TimeTableClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<DayClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<HourClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<ExtraPriceSettingClient>(client =>
            {
                RegisterClient(builder, client);
            });
            builder.Services.AddHttpClient<UserClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            InitalizeLocalization(builder);

            await builder.Build().RunAsync();
        }
    }
}
