using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using CarCareApplication.Models;
using CarCareApplication.Resources;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Orders
{
    public class ClientOrderViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableRangeCollection<OrderGroup> ClientOrderGroups { get; private set; } = new ObservableRangeCollection<OrderGroup>();

        public TransactionClient Client { get; set; }
        public ClientOrderViewModel()
        {
            LoadCommand = new Command(async () =>
            {
                if (GlobalResources.Current.ApplicationUser.IsUserLoggedIn)
                {
                    Client = new TransactionClient(App.HttpClient);
                    CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> commitResult = await Client.GetAsync(GlobalResources.Current.ApplicationUser.Id, App.LangCode);
                    if (commitResult.IsSuccess)
                    {
                        ClientOrderGroups.Clear();
                        foreach (var item in commitResult.Value)
                        {
                            ClientOrderGroups.Add(new OrderGroup(item.Key, new ObservableRangeCollection<IndexTransactionViewModel>(item.Value)));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                    }

                }
            });

            CancelCommand = new Command<IndexTransactionViewModel>(async (order) =>
            {

                CommitResult commitResult = await Client.DeleteAsnyc(order.Id);

                if (commitResult.IsSuccess)
                {
                    IsRunning = true;

                    CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> commitResult2 = await Client.GetAsync(GlobalResources.Current.ApplicationUser.Id, App.LangCode);
                    if (commitResult2.IsSuccess)
                    {
                        ClientOrderGroups.Clear();
                        foreach (var item in commitResult2.Value)
                        {
                            ClientOrderGroups.Add(new OrderGroup(item.Key, new ObservableRangeCollection<IndexTransactionViewModel>(item.Value)));
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult2.ErrorCode), Language.OK);
                    }
                    IsRunning = false;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
            });
        }


    }
}
