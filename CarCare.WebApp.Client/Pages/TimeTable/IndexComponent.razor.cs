using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using CarCareApplication.WebApp.Client.Utility;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.TimeTable
{
    public partial class IndexComponent
    {
        [Inject] public TimeTableClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public List<string> Days { get; set; } = new List<string>();

        public Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>> HourOfWorks { get; set; } = new Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>();
        public List<IndexHourOfWorkViewModel> ItemsSouce { get; set; } = new List<IndexHourOfWorkViewModel>();
        protected async override Task OnInitializedAsync()
        {
            CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>> commitResult = await Client.GetAsync(Program.AppLang);
            if (commitResult.IsSuccess)
            {
                HourOfWorks = commitResult.Value;
                ItemsSouce.AddRange(HourOfWorks.Where(a => a.Key.Equals(HourOfWorks.Keys.FirstOrDefault())).SelectMany(a => a.Value));
                Days.AddRange(HourOfWorks.Keys);
            }
            else
            {

            }

        }

        private async Task ToggleEnable(int Id)
        {
            CommitResult commitResult = await Client.ToggleEnableAsnyc(Id);
            if (commitResult.IsSuccess)
            {
                IndexHourOfWorkViewModel index = HourOfWorks.SelectMany(a => a.Value).SingleOrDefault(a => a.Id.Equals(Id));
                index.IsEnabled = !index.IsEnabled;
                HourOfWorks.SelectMany(a => a.Value).ToList().ReplaceItem((a) => a.Id.Equals(Id), index);
                ToastService.ShowSuccess("Toggole has been done successfully");
            }
            else
            {
                ToastService.ShowError("Toggole couldn't be done correctly, try again later");
            }
        }

        private void OnDayChange(ChangeEventArgs e)
        {
            ItemsSouce.Clear();
            ItemsSouce.AddRange(HourOfWorks.SingleOrDefault(a => a.Key.Equals(e.Value.ToString())).Value);
        }

    }
}
