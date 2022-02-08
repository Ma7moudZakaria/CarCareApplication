using CarCareApplication.Core.Shared.DataTransferObject;
using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.DayModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CarCareApplication.Core.Shared.Repositories
{
    public class DayRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public DayRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IEnumerable<SelectItemList>>> GetSelectItemsAsync(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<SelectItemList>>
                    {
                        IsSuccess = false,
                        ErrorCode = "DY-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<IEnumerable<SelectItemList>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Day>().Where(a => a.IsEnabled).Select(a => new SelectItemList { Value = a.Id, Text = langCode.Equals("ar") ? a.NameAR : a.NameEN }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<SelectItemList>>
                {
                    IsSuccess = false,
                    ErrorCode = "DY-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexDayViewModel>>> GetDaysAsync(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<IndexDayViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "DY-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                return new CommitResult<IEnumerable<IndexDayViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Day>().Where(a => a.IsEnabled)
                                            .Select(a => new IndexDayViewModel { Id = a.Id, Name = langCode.Equals("ar") ? a.NameAR : a.NameEN, IsEnabled = a.IsEnabled }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexDayViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "DY-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
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
                        ErrorCode = "DY-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
                Day day = await _dbContext.Set<Day>().FindAsync(Id);

                if (day is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "DY-X0009",
                        ErrorType = ErrorType.Error
                    };
                }
                day.IsEnabled = !day.IsEnabled;
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
                    ErrorCode = "DY-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
