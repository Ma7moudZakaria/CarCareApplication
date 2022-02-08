using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.AddressModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class AddressRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public AddressRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IEnumerable<IndexAddressViewModel>>> GetAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return new CommitResult<IEnumerable<IndexAddressViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<IEnumerable<IndexAddressViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().Where(a => a.IsEnabled && a.UserId == userId)
                            .Select(a => new IndexAddressViewModel
                            {
                                Id = a.Id,
                                FullAddress = a.FullAddress,
                                Name = a.Name,
                                PhoneNumber = a.PhoneNumber,
                                Type = a.Type,
                                KilometerAway = a.KilometersAway
                            }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexAddressViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "AD-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateAsync(CreateAddressViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0003",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.UserId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0004",
                        ErrorType = ErrorType.Error
                    };
                }
                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0005",
                        ErrorType = ErrorType.Error,
                    };
                }
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0006",
                        ErrorType = ErrorType.Error,
                    };
                }
                if (string.IsNullOrWhiteSpace(model.FullAddress))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0007",
                        ErrorType = ErrorType.Error,
                    };
                }
                if (model.KilometersAway < 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0008",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Latitude <= 0 || model.Latitude <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0009",
                        ErrorType = ErrorType.Error
                    };
                }

                _dbContext.Set<Address>().Add(new Address
                {
                    Name = model.Name,
                    FullAddress = model.FullAddress,
                    PhoneNumber = model.PhoneNumber,
                    Type = model.Type,
                    UserId = model.UserId,
                    KilometersAway = model.KilometersAway,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    IsEnabled = true,
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
                    ErrorCode = "AD-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> DeleteAsync(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
                Address address = await _dbContext.Set<Address>().FindAsync(Id);

                if (address is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "AD-X0010",
                        ErrorType = ErrorType.Error
                    };
                }
                _dbContext.Set<Address>().Remove(address);

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
                    ErrorCode = "AD-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
