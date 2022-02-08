using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.ExpensesModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Expenses
{
    public partial class IndexComponent
    {
        [Inject] public ExpenseClient Client { get; set; }

        public List<IndexExpenseViewModel> ItemsSource { get; set; } = new List<IndexExpenseViewModel>();

        protected async override Task OnInitializedAsync()
        {
            CommitResult<IEnumerable<IndexExpenseViewModel>> commitResult = await Client.GetAsync();
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
