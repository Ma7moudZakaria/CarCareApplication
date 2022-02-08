using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.RevenueModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Revenues
{
    public partial class IndexComponent
    {
        [Inject] public RevenueClient Client { get; set; }

        public List<IndexRevenueViewModel> ItemsSource { get; set; } = new List<IndexRevenueViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexRevenueViewModel>> commitResult = await Client.GetAsync();
            if (commitResult.IsSuccess)
            {
                ItemsSource.AddRange(commitResult.Value);
            }
            else
            {

            }
        }
    }
}
