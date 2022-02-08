using CarCareApplication.Android.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CarCareApplication.Android.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}