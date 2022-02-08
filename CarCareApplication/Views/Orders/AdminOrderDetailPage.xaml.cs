using CarCareApplication.ViewModels.Orders;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarCareApplication.Views.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminOrderDetailPage : ContentPage
    {
        public AdminOrderDetailPage()
        {
            AdminOrderDetailViewModel adminOrderDetailViewModel = new AdminOrderDetailViewModel();
            InitializeComponent();
            this.BindingContext = adminOrderDetailViewModel;
            adminOrderDetailViewModel.AppMap = AppMap;
        }
    }
}