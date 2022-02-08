using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.ExtraPriceSettingModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class ExtraPriceSettingRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public ExtraPriceSettingRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;

        }

        public async Task<CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>> GetExtraPriceSettingsAsync(string lang)
        {
            try
            {
                if (string.IsNullOrEmpty(lang))
                {
                    return new CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<ExtraPriceSetting>().Include(a => a.CarType).Include(a => a.Service).Select(a => new IndexExtraPriceSettingViewModel
                    {
                        Id = a.Id,
                        ExtraPrice = a.ExtraPrice,
                        CarType = lang.Equals("ar") ? a.CarType.NameAR : a.CarType.NameEN,
                        Service = lang.Equals("ar") ? a.Service.NameAR : a.Service.NameEN
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexExtraPriceSettingViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "EPS-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateExtraPriceSettingViewModel>> GetExtraPriceSettingForUpdateAsync(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult<UpdateExtraPriceSettingViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                ExtraPriceSetting extraPriceSetting = await _dbContext.Set<ExtraPriceSetting>().FindAsync(Id);

                if (extraPriceSetting is null)
                {
                    return new CommitResult<UpdateExtraPriceSettingViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<UpdateExtraPriceSettingViewModel>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = new UpdateExtraPriceSettingViewModel
                    {
                        Id = extraPriceSetting.Id,
                        ExtraPrice = extraPriceSetting.ExtraPrice,
                        CarTypeId = extraPriceSetting.CarTypeId,
                        ServiceId = extraPriceSetting.ServiceId
                    }
                };
            }
            catch
            {
                return new CommitResult<UpdateExtraPriceSettingViewModel>
                {
                    IsSuccess = false,
                    ErrorCode = "EPS-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateExtraPriceSettingAsync(CreateExtraPriceSettingViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0005",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.CarTypeId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0006",
                        ErrorType = ErrorType.Error
                    };
                }
                if(model.ServiceId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0007",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.ExtraPrice < 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0008",
                        ErrorType = ErrorType.Error
                    };
                }
                ExtraPriceSetting result = await _dbContext.Set<ExtraPriceSetting>().SingleOrDefaultAsync(a => a.ServiceId.Equals(model.ServiceId) && a.CarTypeId.Equals(model.CarTypeId));

                if (result is not null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0004",
                        ErrorType = ErrorType.Error
                    };
                }             

                _dbContext.Set<ExtraPriceSetting>().Add(new ExtraPriceSetting
                {
                    ExtraPrice = model.ExtraPrice,
                    CarTypeId = model.CarTypeId,
                    ServiceId = model.ServiceId,
                    IsEnabled =  true
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
                    ErrorCode = "EPS-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> UpdateExtraPriceSettingAsync(UpdateExtraPriceSettingViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0009",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.CarTypeId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0006",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.ServiceId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0007",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.ExtraPrice <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0008",
                        ErrorType = ErrorType.Error
                    };
                }
                ExtraPriceSetting extraPriceSetting = await _dbContext.Set<ExtraPriceSetting>().FindAsync(model.Id);

                if (extraPriceSetting is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0004",
                        ErrorType = ErrorType.Error
                    };
                }

                extraPriceSetting.ExtraPrice = model.ExtraPrice;
                extraPriceSetting.CarTypeId = model.CarTypeId;
                extraPriceSetting.ServiceId = model.ServiceId;

                _dbContext.Set<ExtraPriceSetting>().Update(extraPriceSetting);
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
                    ErrorCode = "EPS-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> DeleteExtraPriceSettingAsync(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                ExtraPriceSetting extraPriceSetting = await _dbContext.Set<ExtraPriceSetting>().FindAsync(Id);
                if (extraPriceSetting is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "EPS-X0004",
                        ErrorType = ErrorType.Error
                    };
                }
                extraPriceSetting.IsEnabled = false;
                _dbContext.Set<ExtraPriceSetting>().Update(extraPriceSetting);
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
                    ErrorCode = "EPS-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
