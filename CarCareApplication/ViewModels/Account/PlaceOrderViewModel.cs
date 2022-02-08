using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using CarCareApplication.Resources;
using CarCareApplication.Views.Accounts;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarCareApplication.ViewModels.Account
{
    public class PlaceOrderViewModel : BaseViewModel
    {
        public ICommand PlaceOrderCommand { get; set; }

        public TransactionClient Client { get; set; }
        public PlaceOrderViewModel()
        {
            PlaceOrderCommand = new Command(async () =>
            {
                if (Request.Current.HourOfWorkId == 0 || Request.Current.CarTypeId == 0 || Request.Current.ServiceId == 0 || Request.Current.AddressId == 0)
                {
                    await Shell.Current.GoToAsync("//HomePage");
                    return;
                }

                Client = new TransactionClient(App.HttpClient);

                CommitResult commitResult = await Client.CreateAsync(new CreateTransactionViewModel
                {
                    AddressId = Request.Current.AddressId,
                    BasePrice = Request.Current.Price + Request.Current.DeliveryPrice,
                    CarTypeId = Request.Current.CarTypeId,
                    HourOfWorkId = Request.Current.HourOfWorkId,
                    ServiceId = Request.Current.ServiceId,
                });

                if (commitResult.IsSuccess)
                {
                    Request.Current = new Request();
                    await Shell.Current.GoToAsync(nameof(OrderDonePage));
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert(Language.Error, Language.ResourceManager.GetString(commitResult.ErrorCode), Language.OK);
                }
            });
        }
    }
}
