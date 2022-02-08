using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class RoleRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public RoleRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IEnumerable<RoleViewModel>>> GetRolesAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<RoleViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Role>().Select(a => new RoleViewModel
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<RoleViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "RO-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }
    }
}
