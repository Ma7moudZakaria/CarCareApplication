using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.AddressModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly AddressRepo _addressRepo;
        public AddressController(AddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressViewModel model)
               => Ok(await _addressRepo.CreateAsync(model));

        [HttpGet("{UserId:int}")]
        public async Task<IActionResult> GetAllAddresses(int UserId)
               => Ok(await _addressRepo.GetAsync(UserId));

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteAddress(int Id)
               => Ok(await _addressRepo.DeleteAsync(Id));
    }
}
