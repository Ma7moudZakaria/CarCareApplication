using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.ServiceModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class ServiceRepo
    {
        private CarCareApplicationDbContext _dbContext;

        public ServiceRepo(CarCareApplicationDbContext db)
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
                        ErrorCode = "SE-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                Service service = await _dbContext.Set<Service>().FindAsync(Id);

                if (service is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0002",
                        ErrorType = ErrorType.Error
                    };
                }

                service.IsEnabled = !service.IsEnabled;

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
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexServiceViewModel>>> GetServicesAsync(string langCode, int carType)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<IndexServiceViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (carType <= 0)
                {
                    return new CommitResult<IEnumerable<IndexServiceViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0005",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                CarType type = await _dbContext.Set<CarType>().SingleOrDefaultAsync(a => a.Id.Equals(carType) && a.IsEnabled);

                if (type is null)
                {
                    return new CommitResult<IEnumerable<IndexServiceViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0006",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                return new CommitResult<IEnumerable<IndexServiceViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<ExtraPriceSetting>().Where(a => a.CarTypeId.Equals(carType)).Include(a => a.Service).Select(a => new IndexServiceViewModel
                    {
                        Id = a.Service.Id,
                        Name = langCode.Equals("ar") ? a.Service.NameAR : a.Service.NameEN,
                        BasePrice = a.Service.BasePrice + a.ExtraPrice,
                        ServingMinutes = a.Service.ServingMinutes,
                        IsEnabled = a.Service.IsEnabled,
                        Description = langCode.Equals("ar") ? a.Service.DescriptionAR : a.Service.DescriptionEN,
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexServiceViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexServiceViewModel>>> GetAsync(string langCode,bool isMobile)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<IndexServiceViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                return new CommitResult<IEnumerable<IndexServiceViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Service>().Where(a=> isMobile ? a.IsEnabled : true).Select(a => new IndexServiceViewModel
                    {
                        Id = a.Id,
                        Name = langCode.Equals("ar") ? a.NameAR : a.NameEN,
                        BasePrice = a.BasePrice,
                        ServingMinutes = a.ServingMinutes,
                        IsEnabled = a.IsEnabled,
                        Description = langCode.Equals("ar") ? a.DescriptionAR : a.DescriptionEN
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexServiceViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateServiceViewModel>> GetServiceForUpdateAsync(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult<UpdateServiceViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Service service = await _dbContext.Set<Service>().FindAsync(Id);

                if (service is null)
                {
                    return new CommitResult<UpdateServiceViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<UpdateServiceViewModel>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = new UpdateServiceViewModel
                    {
                        Id = service.Id,
                        NameAR = service.NameAR,
                        NameEN = service.NameEN,
                        BasePrice = service.BasePrice,
                        ServingMinutes = service.ServingMinutes,
                        IsEnabled = service.IsEnabled,
                        DescriptionAR =service.DescriptionAR,
                        DescriptionEN = service.DescriptionEN
                    }
                };
            }
            catch
            {
                return new CommitResult<UpdateServiceViewModel>
                {
                    IsSuccess = false,
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateServiceAsync(CreateServiceViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SER-X0007",
                        ErrorType = ErrorType.Error
                    };
                }

                if (model.BasePrice <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0008",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.NameAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0009",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.NameEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0010",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.DescriptionAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0013",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.DescriptionEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0014",
                        ErrorType = ErrorType.Error
                    };
                }
                Service result = await _dbContext.Set<Service>().SingleOrDefaultAsync(a => a.NameAR.Equals(model.NameAR) || a.NameEN.Equals(model.NameEN));

                if (result is not null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0011",
                        ErrorType = ErrorType.Error
                    };
                }

                _dbContext.Set<Service>().Add(new Service
                {
                    NameAR = model.NameAR,
                    NameEN = model.NameEN,
                    ServingMinutes = model.ServingMinutes,
                    DescriptionAR = model.DescriptionAR,
                    DescriptionEN = model.DescriptionEN,
                    BasePrice = model.BasePrice,
                    IsEnabled = model.IsEnabled
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
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> UpdateServiceAsync(UpdateServiceViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0012",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SER-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.BasePrice <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0008",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.NameAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0009",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.NameEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0010",
                        ErrorType = ErrorType.Error
                    };
                }
                if (string.IsNullOrEmpty(model.DescriptionAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0013",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrEmpty(model.DescriptionEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0014",
                        ErrorType = ErrorType.Error
                    };
                }
                Service service = await _dbContext.Set<Service>().FindAsync(model.Id);

                if (service is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "SE-X0002",
                        ErrorType = ErrorType.Error
                    };
                }
                service.NameAR = model.NameAR;
                service.NameEN = model.NameEN;
                service.BasePrice = model.BasePrice;
                service.ServingMinutes = model.ServingMinutes;
                model.DescriptionEN = model.DescriptionEN;
                model.DescriptionAR = model.DescriptionAR;
                _dbContext.Set<Service>().Update(service);
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
                    ErrorCode = "SE-X0003",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
