using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using CarCareApplication.Models;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class AppointmentViewModel : BaseViewModel
    {
        public ObservableRangeCollection<AppointmentGroup> AppointmentGroups { get; set; } = new ObservableRangeCollection<AppointmentGroup>();

        public ICommand LoadCommand { get; set; }
        public ICommand SelectAppointmentCommand { get; set; }

        public TimeTableClient Client { get; set; }

        public AppointmentViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                IsRunning = true;
                Client = new TimeTableClient(App.HttpClient);
                CommitResult<Dictionary<string,IEnumerable<IndexHourOfWorkViewModel>>> commitResult = await Client.GetAsync(Request.Current.ServiceId, App.LangCode);
                if (commitResult.IsSuccess)
                {
                    AppointmentGroups.Clear();
                    foreach (var appointment in commitResult.Value)
                    {
                        if (appointment.Value.Any(a => a.IsAvailable))
                        {
                            AppointmentGroups.Add(new AppointmentGroup(appointment.Key, new ObservableRangeCollection<IndexHourOfWorkViewModel>(appointment.Value.Where(a => a.IsAvailable).ToArray())));
                        }
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                IsRunning = false;
            });

            SelectAppointmentCommand = new Command<int>(async (Id) =>
            {
                Request.Current.HourOfWorkId = Id;
                await Shell.Current.GoToAsync(nameof(AddressPage));
            });
        }

    }
}
