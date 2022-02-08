using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionRepo _transactionRepo;
        public TransactionController(TransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionViewModel model)
            => Ok(await _transactionRepo.CreateOrderAsync(model));


        ///TODO
        // Retun list of Transaction grouped by date their status is enabled. (Car Type , Service Type , Kilometers away, Total Cash, Service Time).

        [HttpGet("{langCode}")]
        public IActionResult GetTransactions(string langCode)
            => Ok(_transactionRepo.GetTransactions(langCode));
       

        [HttpGet("{UserId}/{langCode}")]
        public IActionResult GetTransactions(int UserId, string langCode)
            => Ok(_transactionRepo.GetTransactions(UserId, langCode));
        

        [HttpGet("Detail/{Id:int}/{langCode}")]
        public async Task<IActionResult> GetTransactionDetail(int Id, string langCode)
            => Ok(await _transactionRepo.GetTransactionDetailById(Id, langCode));


        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateTransaction(int Id) // edit = a => done , edit=> c => remove
             => Ok(await _transactionRepo.DoneTransationAsync(Id));

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteTransaction(int Id) // edit = a => done , edit=> c => remove
             => Ok(await _transactionRepo.CancelTransationAsync(Id));


        // Each transaction have detailes (Car Type , Service Type , Kilometers away, Total Cash, Service Time , Person's name, PhoneNumber, base cash, extra cash, Latitude, Longitude)

        // PUT: Cancel/accept an order by Id
    }
}
