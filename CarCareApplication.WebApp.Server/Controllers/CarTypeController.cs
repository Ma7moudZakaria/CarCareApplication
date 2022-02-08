using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class CarTypeController : Controller
    {
        private readonly CarTypeRepo _carTypeRepo;
        public CarTypeController(CarTypeRepo carTypeRepo)
        {
            _carTypeRepo = carTypeRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarType([FromBody] CreateCarTypeViewModel model)
               => Ok(await _carTypeRepo.CreateCarTypeAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateCarType([FromBody] UpdateCarTypeViewModel model)
               => Ok(await _carTypeRepo.UpdateCarTypeAsync(model));

        [HttpGet("{LangCode}/{isMobile:bool}")]
        public async Task<IActionResult> GetAllCarTypes(string LangCode,bool isMobile)
               => Ok(await _carTypeRepo.GetCarTypes(LangCode,isMobile));



        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetCarType(int Id)
               => Ok(await _carTypeRepo.GetCarTypeByIdForUpdate(Id));


        [HttpGet("Toggle/{Id:int}")]
        public async Task<IActionResult> Toggle(int Id)
           => Ok(await _carTypeRepo.ToggleEnable(Id));

    }
}
