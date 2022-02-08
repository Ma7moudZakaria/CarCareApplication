using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages
{
    public partial class Index
    {
        [Inject] RevenueClient RevenueClient { get; set; }
        [Inject] ExpenseClient ExpenseClient { get; set; }

        public float ExpenseCash { get; set; }
        public float RevenueCash { get; set; }
        protected async override Task OnInitializedAsync()
        {
            CommitResult<float> expenseCommitResult = await ExpenseClient.GetRevenueCashAsync();
            if (expenseCommitResult.IsSuccess)
            {
                ExpenseCash = expenseCommitResult.Value;
            }
            else
            {

            }
            CommitResult<float> revenueCommitResult = await RevenueClient.GetRevenueCashAsync();
            if (revenueCommitResult.IsSuccess)
            {
                RevenueCash = revenueCommitResult.Value;
            }
            else
            {

            }
        }
    }
}
