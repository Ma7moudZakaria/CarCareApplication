using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class ServiceViewModel : BaseViewModel
    {
        public ObservableRangeCollection<IndexServiceViewModel> ServiceCards { get; set; } = new ObservableRangeCollection<IndexServiceViewModel>();

        public ICommand ServiceSelectedCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        private IndexServiceViewModel _selectedServiceCard;

        public IndexServiceViewModel SelectedServiceCard
        {
            get { return _selectedServiceCard; }
            set { _selectedServiceCard = value; OnPropertyChanged(nameof(SelectedServiceCard)); }
        }


        public ServiceClient Client { get; set; }

        public ServiceViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                IsRunning = true;
                Client = new ServiceClient(App.HttpClient);
                CommitResult<IEnumerable<IndexServiceViewModel>> commitResult = await Client.GetAsync(App.LangCode , Request.Current.CarTypeId);
                if (commitResult.IsSuccess) {
                    ServiceCards.AddRange(commitResult.Value);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                IsRunning = false;
            });
            ServiceSelectedCommand = new Command(async () =>
            {
                if (SelectedServiceCard is null) return;
                Request.Current.ServiceId = SelectedServiceCard.Id;
                Request.Current.Service = SelectedServiceCard.Name;
                Request.Current.Price = SelectedServiceCard.BasePrice;
                await Shell.Current.GoToAsync(nameof(AppointmentPage));
                SelectedServiceCard = null;
            });
            Task.Run(() =>
            {
                LoadCommand.Execute(null);
            });
        }
    }
}
