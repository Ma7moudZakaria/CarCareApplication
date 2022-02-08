using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.DayModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class DayController : Controller
    {
        private readonly DayRepo _dayRepo;
        public DayController(DayRepo dayRepo)
        {
            _dayRepo = dayRepo;
        }

        [HttpGet("{Language}")]
        public async Task<IActionResult> GetAllDays(string Language)
               => Ok(await _dayRepo.GetDaysAsync(Language));

        [HttpGet("Toggle/{Id:int}")]
        public async Task<IActionResult> Toggle(int Id)
             => Ok(await _dayRepo.ToggleEnable(Id));
    }
}
