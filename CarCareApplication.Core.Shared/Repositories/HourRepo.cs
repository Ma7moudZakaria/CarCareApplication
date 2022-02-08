using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.HourModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class HourRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public HourRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult> ToggleEnable(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "HR-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                Hour hour = await _dbContext.Set<Hour>().FindAsync(Id);

                if (hour is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "HR-X0002",
                        ErrorType = ErrorType.Error
                    };
                }

                hour.IsEnabled = !hour.IsEnabled;

                await _dbContext.SaveChangesAsync();

                return new CommitResult
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None
                };
            }
            catch
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "HR-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<ICollection<IndexHourViewModel>>> GetHoursAsync(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<ICollection<IndexHourViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "HR-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<ICollection<IndexHourViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Hour>().Select(a => new IndexHourViewModel { Id = a.Id, Name = langCode.Equals("ar") ? a.NameAR : a.NameEN, IsEnabled = a.IsEnabled }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<ICollection<IndexHourViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "H-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }
    }
}
