using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using Xamarin.CommunityToolkit.ObjectModel;

namespace CarCareApplication.Models
{
    public class AppointmentGroup : ObservableRangeCollection<IndexHourOfWorkViewModel>
    {
        public string Header { get; set; }
        public AppointmentGroup(string header, ObservableRangeCollection<IndexHourOfWorkViewModel> items) : base(items)
        {
            Header = header;
        }
    }
}
