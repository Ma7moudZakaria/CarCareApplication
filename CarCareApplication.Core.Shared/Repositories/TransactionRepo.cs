using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.TransactionModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class TransactionRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public TransactionRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;

        }

        public CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> GetTransactions(int UserId, string langCode)
        {
            try
            {
                if (UserId <= 0)
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(langCode))
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = _dbContext.Set<Transaction>()
                                   .Where(a => a.IsEnabled && a.Address.UserId.Equals(UserId))
                                   .Include(a => a.CarType)
                                   .Include(a => a.Service)
                                   .Include(a => a.Address)
                                   .Include(a => a.HourOfWork)
                                   .ThenInclude(a => a.Hour)
                                   .Include(a => a.HourOfWork)
                                   .ThenInclude(a => a.Day)
                                   .OrderBy(a => a.HourOfWork.Day.Id)
                                   .ToList()
                                   .GroupBy(a => langCode.Equals("ar") ? a.HourOfWork.Day.NameAR : a.HourOfWork.Day.NameEN).ToDictionary(a => a.Key, a => a.Select(b => new IndexTransactionViewModel
                                   {
                                       BasePrice = b.BasePrice,
                                       Id = b.Id,
                                       CarType = langCode.Equals("ar") ? b.CarType.NameAR : b.CarType.NameEN,
                                       KilometersAway = (float)Math.Round(b.Address.KilometersAway),
                                       ServiceType = langCode.Equals("ar") ? b.Service.NameAR : b.Service.NameEN,
                                       ServiceTime = langCode.Equals("ar") ? b.HourOfWork.Hour.NameAR : b.HourOfWork.Hour.NameEN
                                   }))

                };
            }
            catch
            {
                return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                {
                    IsSuccess = false,
                    ErrorCode = "TR-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>> GetTransactions(string langCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(langCode))
                {
                    return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = _dbContext.Set<Transaction>()
                                   .Where(a => a.IsEnabled)
                                   .Include(a => a.CarType)
                                   .Include(a => a.Service)
                                   .Include(a => a.Address)
                                   .Include(a => a.HourOfWork)
                                   .ThenInclude(a => a.Hour)
                                   .Include(a => a.HourOfWork)
                                   .ThenInclude(a => a.Day)
                                   .OrderBy(a => a.HourOfWork.Day.Id)
                                   .ToList()
                                   .GroupBy(a => langCode.Equals("ar") ? a.HourOfWork.Day.NameAR : a.HourOfWork.Day.NameEN).ToDictionary(a => a.Key, a => a.Select(b => new IndexTransactionViewModel
                                   {
                                       BasePrice = b.BasePrice,
                                       Id = b.Id,
                                       CarType = langCode.Equals("ar") ? b.CarType.NameAR : b.CarType.NameEN,
                                       KilometersAway = (float)Math.Round(b.Address.KilometersAway),
                                       ServiceType = langCode.Equals("ar") ? b.Service.NameAR : b.Service.NameEN,
                                       ServiceTime = langCode.Equals("ar") ? b.HourOfWork.Hour.NameAR : b.HourOfWork.Hour.NameEN
                                   }))
                };
            }
            catch
            {
                return new CommitResult<Dictionary<string, IEnumerable<IndexTransactionViewModel>>>
                {
                    IsSuccess = false,
                    ErrorCode = "TR-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<TransactionDetailViewModel>> GetTransactionDetailById(int Id, string langCode)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult<TransactionDetailViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(langCode))
                {
                    return new CommitResult<TransactionDetailViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<TransactionDetailViewModel>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Transaction>().Where(a => a.Id.Equals(Id)).Include(a => a.CarType).Include(a => a.Service).Include(a => a.Address).Include(a => a.HourOfWork).ThenInclude(a => a.Hour)
                                   .Select(b => new TransactionDetailViewModel
                                   {
                                       BasePrice = b.BasePrice,
                                       Id = b.Id,
                                       CarType = langCode.Equals("ar") ? b.CarType.NameAR : b.CarType.NameEN,
                                       KilometersAway = (float)Math.Round(b.Address.KilometersAway),
                                       ServiceType = langCode.Equals("ar") ? b.Service.NameAR : b.Service.NameEN,
                                       ServiceTime = langCode.Equals("ar") ? b.HourOfWork.Hour.NameAR : b.HourOfWork.Hour.NameEN,
                                       UserName = b.Address.Name,
                                       Latitude = b.Address.Latitude,
                                       Longitude = b.Address.Longitude,
                                       PhoneNumber = b.Address.PhoneNumber,
                                       FullAddress = b.Address.FullAddress
                                   }).SingleOrDefaultAsync()
                };
            }
            catch
            {
                return new CommitResult<TransactionDetailViewModel>
                {
                    IsSuccess = false,
                    ErrorCode = "TR-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateOrderAsync(CreateTransactionViewModel model)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model is null)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0004",
                            ErrorType = ErrorType.Error
                        };
                    }
                    if (model.ServiceId <= 0)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }
                    if (model.CarTypeId <= 0)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0006",
                            ErrorType = ErrorType.Error
                        };
                    }
                    if (model.HourOfWorkId <= 0)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0007",
                            ErrorType = ErrorType.Error
                        };
                    }
                    Service service = await _dbContext.Set<Service>().FindAsync(model.ServiceId);
                    HourOfWork hourOfWork = await _dbContext.Set<HourOfWork>().FindAsync(model.HourOfWorkId);
                    hourOfWork.AvailableMinutes -= service.ServingMinutes;
                    _dbContext.Set<HourOfWork>().Update(hourOfWork);
                    _dbContext.Set<Transaction>().Add(new Transaction
                    {
                        AddressId = model.AddressId,
                        CarTypeId = model.CarTypeId,
                        HourOfWorkId = model.HourOfWorkId,
                        ServiceId = model.ServiceId,
                        IsEnabled = true,
                        BasePrice = model.BasePrice,
                    });
                    await _dbContext.SaveChangesAsync();
                    await tran.CommitAsync();
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
                        ErrorCode = "TR-X0003",
                        ErrorType = ErrorType.Error,
                    };
                }
            }
        }

        public async Task<CommitResult> CancelTransationAsync(int Id)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (Id <= 0)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0001",
                            ErrorType = ErrorType.Error
                        };
                    }

                    Transaction transaction = await _dbContext.Set<Transaction>().FindAsync(Id);
                    if (transaction is null)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0008",
                            ErrorType = ErrorType.Error
                        };
                    }
                    Service service = await _dbContext.Set<Service>().FindAsync(transaction.ServiceId);
                    HourOfWork hourOfWork = await _dbContext.Set<HourOfWork>().FindAsync(transaction.HourOfWorkId);
                    hourOfWork.AvailableMinutes += service.ServingMinutes;
                    transaction.IsEnabled = false;
                    _dbContext.Set<HourOfWork>().Update(hourOfWork);
                    _dbContext.Set<Transaction>().Update(transaction);
                    await _dbContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new CommitResult
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None
                    };
                }
                catch
                {
                    await tran.RollbackAsync();
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0003",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult> DoneTransationAsync(int Id)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (Id <= 0)
                    {
                        await tran.RollbackAsync();
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0001",
                            ErrorType = ErrorType.Error
                        };
                    }
                    Transaction transaction = await _dbContext.Set<Transaction>().FindAsync(Id);
                    if (transaction is null)
                    {
                        return new CommitResult
                        {
                            IsSuccess = false,
                            ErrorCode = "TR-X0008",
                            ErrorType = ErrorType.Error
                        };
                    }
                    transaction.IsEnabled = false;
                    _dbContext.Update(transaction);
                    _dbContext.Set<Revenue>().Add(new Revenue
                    {
                        Cash = transaction.BasePrice,
                        CashType = CashType.BasePrice,
                        Description = $"Income for Base Price - Transaction Id: {transaction.Id}",
                    });
                    return new CommitResult
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None
                    };
                }
                catch
                {
                    await tran.RollbackAsync();
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "TR-X0003",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }
    }
}
