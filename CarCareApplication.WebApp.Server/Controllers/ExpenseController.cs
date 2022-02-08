using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.ExpensesModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseRepo _expenseRepo;
        public ExpenseController(ExpenseRepo expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseViewModel model)
                => Ok(await _expenseRepo.CreateExpenseAsync(model));


        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
               => Ok(await _expenseRepo.GetExpensesAsync());


        [HttpGet("GetCash")]
        public async Task<IActionResult> GetExpenseCash()
             => Ok(await _expenseRepo.ExpenseReport());
    }
}
