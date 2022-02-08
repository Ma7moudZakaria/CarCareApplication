using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.RevenueModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class RevenueController : ControllerBase
    {
        private readonly RevenueRepo _revenueRepo;
        public RevenueController(RevenueRepo revenueRepo)
        {
            _revenueRepo = revenueRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRevenue([FromBody] CreateRevenueViewModel model)
                => Ok(await _revenueRepo.CreateRevenueAsync(model));

        [HttpGet]
        public async Task<IActionResult> GetAllRevenues()
               => Ok(await _revenueRepo.GetRevenuesAsync());


        [HttpGet("GetCash")]
        public async Task<IActionResult> GetRevenueCash()
              => Ok(await _revenueRepo.RevenueReport());
    }
}
