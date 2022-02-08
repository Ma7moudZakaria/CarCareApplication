using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class SettingController : ControllerBase
    {
        private readonly SettingRepo _settingRepo;
        public SettingController(SettingRepo settingRepo)
        {
            _settingRepo = settingRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSetting([FromBody] CreateSettingViewModel model)
                => Ok(await _settingRepo.CreateSettingAsync(model));

        [HttpPost("CheckAreaSupport")]
        public async Task<IActionResult> CheckAreaSupport([FromBody] CheckAreaSupportViewModel model)
                => Ok(await _settingRepo.CheckAreaSupport(model));

        [HttpPut]
        public async Task<IActionResult> UpdateSetting([FromBody] UpdateSettingViewModel model)
               => Ok(await _settingRepo.UpdateSettingAsync(model));



        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetSetting(int Id)
            => Ok(await _settingRepo.GetSettingForUpdateAsync(Id));

        [HttpGet("{langCode}")]
        public async Task<IActionResult> GetAll(string langCode)
            => Ok(await _settingRepo.GetSettingsAsync(langCode));
    }
}
