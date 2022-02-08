using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using CarCareApplication.Models;
using CarCareApplication.Resources;
using CarCareApplication.Views.Orders;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Orders
{
    public class AdminOrderViewModel : BaseViewModel
    {

        public ObservableRangeCollection<OrderGroup> AdminOrderGroups { get; private set; } = new ObservableRangeCollection<OrderGroup>();


        public ICommand LoadCommand { get; set; }

        public ICommand OrderSelectionChanged { get; set; }

        public ICommand RemoveCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        private IndexTransactionViewModel _selectedOrder;

        public IndexTransactionViewModel SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); }
        }

        public TransactionClient Client { get; set; }
        public AdminOrderViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                if (GlobalResources.Current.ApplicationUser.IsUserLoggedIn)
                {
                    IsRunning = true;
                    Client = new TransactionClient(App.HttpClient);
                    CommitResult<Dictionary<string,IEnumerable<IndexTransactionViewModel>>> commitResult = await Client.GetAsync(App.LangCode);
                    if (commitResult.IsSuccess) {
                        AdminOrderGroups.Clear();
                        foreach (var item in commitResult.Value)
                        {
                            AdminOrderGroups.Add(new OrderGroup(item.Key, new ObservableRangeCollection<IndexTransactionViewModel>(item.Value)));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                    }
                    IsRunning = false;

                }
            });

            OrderSelectionChanged = new Command(async () =>
            {
                if (SelectedOrder is null) return;
                await Shell.Current.GoToAsync($"{nameof(AdminOrderDetailPage)}?Id={SelectedOrder.Id}");
                SelectedOrder = null;
            });

            DoneCommand = new Command<int>(async (Id) =>
            {
                CommitResult commitResult = await Client.DoneAsnyc(Id);
                if (commitResult.IsSuccess)
                {
                    CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> commitResult2 = await Client.GetAsync(App.LangCode);
                    if (commitResult2.IsSuccess)
                    {
                        AdminOrderGroups.Clear();
                        foreach (var item in commitResult2.Value)
                        {
                            AdminOrderGroups.Add(new OrderGroup(item.Key, new ObservableRangeCollection<IndexTransactionViewModel>(item.Value)));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult2.ErrorCode), Language.OK);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }

            });

            RemoveCommand = new Command<int>(async (Id) =>
            {
                CommitResult commitResult = await Client.DeleteAsnyc(Id);

                if (commitResult.IsSuccess)
                {
                    CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> commitResult2 = await Client.GetAsync(App.LangCode);
                    if (commitResult2.IsSuccess)
                    {
                        AdminOrderGroups.Clear();
                        foreach (var item in commitResult2.Value)
                        {
                            AdminOrderGroups.Add(new OrderGroup(item.Key, new ObservableRangeCollection<IndexTransactionViewModel>(item.Value)));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult2.ErrorCode), Language.OK);
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
                // Make Http Request 
            });
        }
    }
}
