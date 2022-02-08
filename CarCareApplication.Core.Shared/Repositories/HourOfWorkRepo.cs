using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.HourOfWorkModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class HourOfWorkRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public HourOfWorkRepo(CarCareApplicationDbContext db)
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
                        ErrorCode = "HOW-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                HourOfWork hourOfWork = await _dbContext.Set<HourOfWork>().FindAsync(Id);

                if (hourOfWork is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "HOW-X0002",
                        ErrorType = ErrorType.Error
                    };
                }
                hourOfWork.IsEnabled = !hourOfWork.IsEnabled;
                hourOfWork.IsForcedEnabled = hourOfWork.IsEnabled;
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
                    ErrorCode = "HOW-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>> GetHourOfWorksAsync(string langCode, int serviceId)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "HOW-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (serviceId <= 0)
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "HOW-X0005",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Service service = await _dbContext.Set<Service>().FindAsync(serviceId);

                if (service is null)
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "HOW-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                List<HourOfWork> hourOfWorks = await _dbContext.Set<HourOfWork>().Include(a => a.Hour).Include(a => a.Day).ToListAsync();

                return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = hourOfWorks.GroupBy(a => langCode.Equals("ar") ? a.Day.NameAR : a.Day.NameEN)
                            .ToDictionary(a => a.Key, b => b.Select(a => new IndexHourOfWorkViewModel
                            {
                                Id = a.Id,
                                Name = langCode.Equals("ar") ? a.Hour.NameAR : a.Hour.NameEN,
                                IsAvailable = ValidateTime(a.Day, a.Hour.Start, a.Hour.End, a.AvailableMinutes, service.ServingMinutes).Value && ((a.Hour.IsEnabled && a.Day.IsEnabled && a.IsEnabled) || a.IsForcedEnabled),
                                IsEnabled = (a.Hour.IsEnabled && a.Day.IsEnabled && a.IsEnabled) || a.IsForcedEnabled
                            }))
                };
            }
            catch
            {
                return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                {
                    IsSuccess = false,
                    ErrorCode = "HOW-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>> GetHourOfWorksAsync(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "HOW-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                List<HourOfWork> hourOfWorks = await _dbContext.Set<HourOfWork>().Include(a => a.Hour).Include(a => a.Day).ToListAsync();

                return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = hourOfWorks.GroupBy(a => langCode.Equals("ar") ? a.Day.NameAR : a.Day.NameEN)
                            .ToDictionary(a => a.Key, b => b.Select(a => new IndexHourOfWorkViewModel
                            {
                                Id = a.Id,
                                Name = langCode.Equals("ar") ? a.Hour.NameAR : a.Hour.NameEN,
                                IsAvailable = (a.Hour.IsEnabled && a.Day.IsEnabled && a.IsEnabled) || a.IsForcedEnabled,
                                IsEnabled = (a.Hour.IsEnabled && a.Day.IsEnabled && a.IsEnabled) || a.IsForcedEnabled
                            }))
                };
            }
            catch
            {
                return new CommitResult<Dictionary<string, IEnumerable<IndexHourOfWorkViewModel>>>
                {
                    IsSuccess = false,
                    ErrorCode = "HOW-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        private CommitResult<bool> ValidateTime(Day day, TimeSpan start, TimeSpan end, int availableMinutes, int servingMinutes)
        {
            try
            {
                DateTime currentDate = DateTime.UtcNow.AddHours(2);
                // Check for the current day
                if (currentDate.DayOfWeek == day.DayOfWeek)
                {
                    // Check if the current time is between start and end, then it must update the total availble minutes
                    if (currentDate.TimeOfDay >= start && currentDate.TimeOfDay <= end)
                    {
                        return new CommitResult<bool>
                        {
                            IsSuccess = true,
                            ErrorCode = string.Empty,
                            ErrorType = ErrorType.None,
                            Value = (availableMinutes - Math.Round((currentDate.TimeOfDay - start).TotalMinutes)) >= servingMinutes
                        };
                    }
                    if (currentDate.TimeOfDay >= end)
                    {
                        return new CommitResult<bool>
                        {
                            IsSuccess = false,
                            ErrorCode = string.Empty,
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }
                    if (currentDate.TimeOfDay <= start)
                    {
                        return new CommitResult<bool>
                        {
                            IsSuccess = true,
                            ErrorCode = string.Empty,
                            ErrorType = ErrorType.None,
                            Value = availableMinutes >= servingMinutes
                        };
                    }

                    return new CommitResult<bool>
                    {
                        IsSuccess = false,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.Error
                    };
                }
                else
                {
                    return new CommitResult<bool>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = availableMinutes >= servingMinutes
                    };
                }
            }
            catch
            {
                return new CommitResult<bool>
                {
                    IsSuccess = false,
                    ErrorCode = "HOW-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> ResetHourOfWorkAsync()
        {
            try
            {
                foreach (HourOfWork item in _dbContext.Set<HourOfWork>().Include(a => a.Day).ToList().Where(a => a.Day.DayOfWeek == DateTime.UtcNow.AddHours(2).DayOfWeek))
                {
                    item.AvailableMinutes = 120;
                }

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
                    ErrorCode = "HOW-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
