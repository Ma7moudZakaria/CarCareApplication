using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class HourOfWorkController : Controller
    {
        private readonly HourOfWorkRepo _hourOfWorkRepo;
        public HourOfWorkController(HourOfWorkRepo hourOfWorkRepo)
        {
            _hourOfWorkRepo = hourOfWorkRepo;
        }


        [HttpGet("{LangCode}/{ServiceId:int}")]
        public async Task<IActionResult> GetAllHourOfWorks(string LangCode, int ServiceId)
               => Ok(await _hourOfWorkRepo.GetHourOfWorksAsync(LangCode, ServiceId));

        [HttpGet("{LangCode}")]
        public async Task<IActionResult> GetAllHourOfWorks(string LangCode)
       => Ok(await _hourOfWorkRepo.GetHourOfWorksAsync(LangCode));



        [HttpGet("Toggle/{Id:int}")]
        public async Task<IActionResult> Toggle(int Id)
              => Ok(await _hourOfWorkRepo.ToggleEnable(Id));

    }
}
