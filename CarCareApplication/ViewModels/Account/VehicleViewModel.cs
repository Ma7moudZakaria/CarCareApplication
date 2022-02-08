using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class VehicleViewModel : BaseViewModel
    {
        public ObservableRangeCollection<IndexCarTypeViewModel> CarTypes { get; set; } = new ObservableRangeCollection<IndexCarTypeViewModel>();

        public ICommand CarTypeSelectedCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        private IndexCarTypeViewModel _selectedCarType;

        public IndexCarTypeViewModel SelectedCarType
        {
            get { return _selectedCarType; }
            set { _selectedCarType = value; OnPropertyChanged(nameof(SelectedCarType)); }
        }

        public CarTypeClient Client { get; set; }

        public VehicleViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                IsRunning = true;
                Client = new CarTypeClient(App.HttpClient);
                CommitResult<IEnumerable<IndexCarTypeViewModel>> commitResult = await Client.GetAsync(App.LangCode,true);
                if (commitResult.IsSuccess)
                {
                    CarTypes.AddRange(commitResult.Value);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                IsRunning = false;
            });


            CarTypeSelectedCommand = new Command(async () =>
            {
                if (SelectedCarType is null) return;
                Request.Current.CarTypeId = SelectedCarType.Id;
                Request.Current.CarType = SelectedCarType.Name;
                await Shell.Current.GoToAsync(nameof(ServicePage));
                SelectedCarType = null;
            });

            Task.Run(() =>
            {
                LoadCommand.Execute(null);
            });
        }

    }
}
