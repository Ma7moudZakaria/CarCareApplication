using Blazored.Toast.Services;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.HttpClients;
using CarCareApplication.Core.Shared.ViewModels.ExpensesModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Client.Pages.Expenses
{
    public partial class AddComponent
    {
        public CreateExpenseViewModel ViewModel { get; set; } = new CreateExpenseViewModel();

        [Inject] public ExpenseClient Client { get; set; }
        [Inject] IToastService ToastService { get; set; }

        public async Task OnValidSubmit()
        {
            CommitResult commitResult = await Client.CreateAsync(ViewModel);
            if (commitResult.IsSuccess)
            {
                ToastService.ShowSuccess(string.Empty, Loc["ExpenseSuccess"]);
            }
            else
            {
                ToastService.ShowError(commitResult.ErrorType.ToString(), Loc[commitResult.ErrorCode]);
            }
        }
    }
}
