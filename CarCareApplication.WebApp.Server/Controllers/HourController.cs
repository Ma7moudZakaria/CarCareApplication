using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.HourModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class HourController : Controller
    {
        private readonly HourRepo _hourRepo;
        public HourController(HourRepo hourRepo)
        {
            _hourRepo = hourRepo;
        }

        [HttpGet("{Language}")]
        public async Task<IActionResult> GetAllHours(string Language = "ar")
               => Ok(await _hourRepo.GetHoursAsync(Language));


        [HttpGet("Toggle/{Id:int}")]
        public async Task<IActionResult> Toggle(int Id)
              => Ok(await _hourRepo.ToggleEnable(Id));
    }
}
