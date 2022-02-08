using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.ExpensesModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class ExpenseRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public ExpenseRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IEnumerable<IndexExpenseViewModel>>> GetExpensesAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexExpenseViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Expense>().Select(a => new IndexExpenseViewModel
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
                return new CommitResult<IEnumerable<IndexExpenseViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "EX-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateExpenseAsync(CreateExpenseViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EX-X0002",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EX-X0003",
                        ErrorType = ErrorType.Error
                    };
                }

                if (model.Cash <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EX-X0004",
                        ErrorType = ErrorType.Error
                    };
                }
                _dbContext.Set<Expense>().Add(new Expense
                {
                    Description = model.Description,
                    Cash = model.Cash,
                    CashType = CashType.BasePrice,
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
                    ErrorCode = "EX-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<float>> ExpenseReport()
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
                    Value = await _dbContext.Set<Expense>().Where(a => a.ModifiedDate >= firstDayOfMonth && a.ModifiedDate <= lastDayOfMonth).SumAsync(a => a.Cash)
                };
            }
            catch
            {
                return new CommitResult<float>
                {
                    IsSuccess = false,
                    ErrorCode = "EX-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

    }
}
