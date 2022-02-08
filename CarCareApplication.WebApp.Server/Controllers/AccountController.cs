using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.Repositories;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using CarCareApplication.WebApp.Server.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarCareApplication.WebApp.Server.Controllers
{
    [Route("api/[controller]"), ApiController, AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserRepo _userRepo;
        private readonly RoleRepo _roleRepo;
        public IConfiguration Configuration { get; set; }
        public AccountController(UserRepo userRepo, RoleRepo roleRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            Configuration = configuration;
        }

        // POST : SIGN UP (user data (FirstNAme , second name, phone number, password))
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupUserViewModel model)
        {
            CommitResult<User> commitResult = await _userRepo.SignupAsync(model);

            if (commitResult.IsSuccess)
            {
                return Ok(new CommitResult<TokenResult>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value =  new TokenResult
                    {
                        Token = TokenManager.GenerateToken(Configuration, commitResult.Value, DateTime.UtcNow.AddYears(5), DateTime.UtcNow),
                        UserType = commitResult.Value.Role.Name,
                        UserId = commitResult.Value.Id,
                    }
                });
            }
            else
            {
                return Ok(new CommitResult<TokenResult>
                {
                    IsSuccess = false,
                    ErrorCode = commitResult.ErrorCode,
                    ErrorType = ErrorType.Error,
                    Value = default
                });
            }
        }


        // POST : Sign in (Phone number, password) => (user type)
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninUserViewModel model)
        {
            CommitResult<User> commitResult = await _userRepo.SigninAsync(model);

            if (commitResult.IsSuccess)
            {
                return Ok(new CommitResult<TokenResult>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = new TokenResult
                    {
                        Token = TokenManager.GenerateToken(Configuration, commitResult.Value, DateTime.UtcNow.AddYears(5), DateTime.UtcNow),
                        UserType = commitResult.Value.Role.Name,
                        UserId = commitResult.Value.Id,
                    }
                });
            }
            else
            {
                return Ok(new CommitResult<TokenResult>
                {
                    IsSuccess = false,
                    ErrorCode = commitResult.ErrorCode,
                    ErrorType = ErrorType.Error,
                    Value = default
                });
            }
        }

        // POST : Sign in (Phone number, password) => (user type)
        [HttpGet("get-all-roles")]
        public async Task<IActionResult> Roles()
            => Ok(await _roleRepo.GetRolesAsync());
    }
}
