using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.SettingModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class SettingRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public SettingRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;

        }

        public async Task<CommitResult<UpdateSettingViewModel>> GetSettingForUpdateAsync(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult<UpdateSettingViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Setting setting = await _dbContext.Set<Setting>().FindAsync(Id);

                if (setting is null)
                {
                    return new CommitResult<UpdateSettingViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<UpdateSettingViewModel>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = new UpdateSettingViewModel
                    {
                        Id = setting.Id,
                        KilometerMax = setting.KilometerMax,
                        KilometerMin = setting.KilometerMin,
                        KilometerRate = setting.KilometerRate,
                        ServiceId = setting.ServiceId,
                        CarTypeId = setting.CarTypeId
                    }
                };
            }
            catch
            {
                return new CommitResult<UpdateSettingViewModel>
                {
                    IsSuccess = false,
                    ErrorCode = "SET-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexSettingViewModel>>> GetSettingsAsync(string langCode)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<IndexSettingViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                return new CommitResult<IEnumerable<IndexSettingViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Setting>().Select(a => new IndexSettingViewModel
                    {
                        Id = a.Id,
                        KilometerMax = a.KilometerMax,
                        KilometerMin = a.KilometerMin,
                        KilometerRate = a.KilometerRate,
                        Service = langCode.Equals("ar") ? a.Service.NameAR : a.Service.NameEN,
                        CarType = langCode.Equals("ar") ? a.CarType.NameAR : a.CarType.NameEN
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexSettingViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "SET-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateSettingAsync(CreateSettingViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0005",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.ServiceId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0006",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.CarTypeId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0007",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMax <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0008",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMin <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0009",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMax == model.KilometerMin)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0010",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMin > model.KilometerMax)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0011",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerRate <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0012",
                        ErrorType = ErrorType.Error
                    };
                }

                Setting result = await _dbContext.Set<Setting>().SingleOrDefaultAsync(a => a.ServiceId.Equals(model.ServiceId) && a.CarTypeId.Equals(model.CarTypeId));

                if (result is not null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0014",
                        ErrorType = ErrorType.Error
                    };
                }

                _dbContext.Set<Setting>().Add(new Setting
                {
                    KilometerMax = model.KilometerMax,
                    KilometerMin = model.KilometerMin,
                    KilometerRate = model.KilometerRate,
                    ServiceId = model.ServiceId,
                    CarTypeId = model.CarTypeId
                });

                await _dbContext.SaveChangesAsync();

                return new CommitResult
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None
                };
            }
            catch(Exception ex)
            {
                return new CommitResult
                {
                    IsSuccess = false,
                    ErrorCode = "SET-X0002" + ex.Message + ex.InnerException,
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> UpdateSettingAsync(UpdateSettingViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0005",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.ServiceId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0006",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.CarTypeId <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0007",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMax <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0008",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMin <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0009",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMax == model.KilometerMin)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0010",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerMin > model.KilometerMax)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0011",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.KilometerRate <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0012",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                Setting setting = await _dbContext.Set<Setting>().FindAsync(model.Id);

                if (setting is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0002",
                        ErrorType = ErrorType.Error
                    };
                }

                setting.KilometerMax = model.KilometerMax;
                setting.KilometerMin = model.KilometerMin;
                setting.KilometerRate = model.KilometerRate;
                setting.ServiceId = model.ServiceId;
                setting.CarTypeId = model.CarTypeId;

                _dbContext.Set<Setting>().Update(setting);
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
                    ErrorCode = "SET-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }
        public async Task<CommitResult<float>> CheckAreaSupport(CheckAreaSupportViewModel viewModel)
        {
            try
            {
                Setting setting = await _dbContext.Set<Setting>().SingleOrDefaultAsync(a => a.ServiceId.Equals(viewModel.ServiceId) && a.CarTypeId.Equals(viewModel.CarTypeId));
                if (setting is null)
                {
                    return new CommitResult<float>
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (viewModel.KilometerAway < setting.KilometerMin) // FREE
                {
                    return new CommitResult<float>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = 0
                    };
                }
                if (viewModel.KilometerAway >= setting.KilometerMin && viewModel.KilometerAway <= setting.KilometerMax)
                {
                    return new CommitResult<float>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = viewModel.KilometerAway * setting.KilometerRate
                    };
                }
                else
                {
                    return new CommitResult<float>
                    {
                        IsSuccess = false,
                        ErrorCode = "SET-X0013",
                        ErrorType = ErrorType.Warring,
                        Value = default
                    };
                }
            }
            catch
            {
                return new CommitResult<float>
                {
                    IsSuccess = false,
                    ErrorCode = "SET-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
