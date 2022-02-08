using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly ServiceRepo _serviceRepo;
        public ServiceController(ServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceViewModel model)
                => Ok(await _serviceRepo.CreateServiceAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceViewModel model)
               => Ok(await _serviceRepo.UpdateServiceAsync(model));

        [HttpGet("GetServicesForCarType/{LangCode}/{CarType:int}")]
        public async Task<IActionResult> GetAllServices(string LangCode, int CarType)
               => Ok(await _serviceRepo.GetServicesAsync(LangCode, CarType));

        [HttpGet("{LangCode}/{isMobile:bool}")]
        public async Task<IActionResult> GetAll(string langCode,bool isMobile)
            => Ok(await _serviceRepo.GetAsync(langCode,isMobile));

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetService(int Id)
          => Ok(await _serviceRepo.GetServiceForUpdateAsync(Id));

        [HttpGet("Toggle/{Id:int}")]
        public async Task<IActionResult> Toggle(int Id)
             => Ok(await _serviceRepo.ToggleEnable(Id));
    }
}
