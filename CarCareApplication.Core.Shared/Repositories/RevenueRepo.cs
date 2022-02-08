using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.RevenueModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class RevenueRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public RevenueRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;

        }


        public async Task<CommitResult<IEnumerable<IndexRevenueViewModel>>> GetRevenuesAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexRevenueViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Revenue>().Where(a => a.IsEnabled == true)
                                                     .Select(a => new IndexRevenueViewModel
                                                     {
                                                         Id = a.Id,
                                                         Description = a.Description,
                                                         Cash = a.Cash,
                                                         Date = a.ModifiedDate
                                                     }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexRevenueViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "RE-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateRevenueAsync(CreateRevenueViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "RE-X0002",
                        ErrorType = ErrorType.Error
                    };
                }
                if (string.IsNullOrEmpty(model.Description))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "RE-X0003",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Cash <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "RE-X0004",
                        ErrorType = ErrorType.Error
                    };
                }           
                _dbContext.Set<Revenue>().Add(new Revenue
                {
                    Description = model.Description,
                    Cash = model.Cash,
                    IsEnabled = true
                });

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
                    ErrorCode = "RE-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<float>> RevenueReport()
        {
            try
            {
                DateTime firstDayOfMonth = new DateTime(DateTime.UtcNow.AddHours(2).Year, DateTime.UtcNow.AddHours(2).Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                return new CommitResult<float>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Revenue>().Where(a => a.ModifiedDate >= firstDayOfMonth && a.ModifiedDate <= lastDayOfMonth).SumAsync(a => a.Cash)
                };
            }
            catch
            {
                return new CommitResult<float>
                {
                    IsSuccess = false,
                    ErrorCode = "RE-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
