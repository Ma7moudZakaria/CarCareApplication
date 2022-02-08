using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class ExtraPriceSettingController : ControllerBase
    {
        private readonly ExtraPriceSettingRepo _extraPriceSettingRepo;
        public ExtraPriceSettingController(ExtraPriceSettingRepo extraPriceSettingRepo)
        {
            _extraPriceSettingRepo = extraPriceSettingRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExtraPriceSetting([FromBody] CreateExtraPriceSettingViewModel model)
                => Ok(await _extraPriceSettingRepo.CreateExtraPriceSettingAsync(model));

        [HttpPut]
        public async Task<IActionResult> UpdateExtraPriceSetting([FromBody] UpdateExtraPriceSettingViewModel model)
               => Ok(await _extraPriceSettingRepo.UpdateExtraPriceSettingAsync(model));

        [HttpGet("{langCode}")]
        public async Task<IActionResult> GetAllExtraPriceSettings(string langCode)
               => Ok(await _extraPriceSettingRepo.GetExtraPriceSettingsAsync(langCode));

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetExtraPriceSetting(int Id)
            => Ok(await _extraPriceSettingRepo.GetExtraPriceSettingForUpdateAsync(Id));

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteExtraPriceSetting(int Id)
           => Ok(await _extraPriceSettingRepo.DeleteExtraPriceSettingAsync(Id));
    }
}
