using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace CarCareApplication.Models
{
    public class OrderGroup : ObservableRangeCollection<IndexTransactionViewModel>
    {
        public string Header { get; set; }
        public OrderGroup(string header, ObservableRangeCollection<IndexTransactionViewModel> clientOrders) : base(clientOrders)
        {
            Header = header;
        }
    }
}
