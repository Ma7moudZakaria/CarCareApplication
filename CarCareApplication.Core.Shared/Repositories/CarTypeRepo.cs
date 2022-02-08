using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.CarTypeModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class CarTypeRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public CarTypeRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IEnumerable<IndexCarTypeViewModel>>> GetCarTypes(string langCode, bool isMobile)
        {
            try
            {
                if (string.IsNullOrEmpty(langCode))
                {
                    return new CommitResult<IEnumerable<IndexCarTypeViewModel>>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<IEnumerable<IndexCarTypeViewModel>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<CarType>().Where(a => isMobile ? a.IsEnabled : true).Select(a => new IndexCarTypeViewModel
                    {
                        Id = a.Id,
                        Description = langCode.Equals("ar") ? a.DescriptionAR : a.DescriptionEN,
                        Name = langCode.Equals("ar") ? a.NameAR : a.NameEN,
                        IsEnabled = a.IsEnabled
                    }).ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCarTypeViewModel>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0002",
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
                        ErrorCode = "CT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
                CarType carType = await _dbContext.Set<CarType>().FindAsync(Id);

                if (carType is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0004",
                        ErrorType = ErrorType.Error
                    };
                }
                carType.IsEnabled = !carType.IsEnabled;
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
                    ErrorCode = "CT-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult<UpdateCarTypeViewModel>> GetCarTypeByIdForUpdate(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    return new CommitResult<UpdateCarTypeViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                CarType carType = await _dbContext.Set<CarType>().FindAsync(Id);

                if (carType is null)
                {
                    return new CommitResult<UpdateCarTypeViewModel>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                return new CommitResult<UpdateCarTypeViewModel>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = new UpdateCarTypeViewModel
                    {
                        Id = carType.Id,
                        DescriptionAR = carType.DescriptionAR,
                        DescriptionEN = carType.DescriptionEN,
                        IsEnabled = carType.IsEnabled,
                        NameAR = carType.NameAR,
                        NameEN = carType.NameEN
                    }
                };
            }
            catch
            {
                return new CommitResult<UpdateCarTypeViewModel>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0002",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult> CreateCarTypeAsync(CreateCarTypeViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0005",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrWhiteSpace(model.NameAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0007",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrWhiteSpace(model.NameEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0008",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrWhiteSpace(model.DescriptionAR))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0009",
                        ErrorType = ErrorType.Error
                    };
                }

                if (string.IsNullOrWhiteSpace(model.DescriptionEN))
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0010",
                        ErrorType = ErrorType.Error
                    };
                }

                CarType result = await _dbContext.Set<CarType>().SingleOrDefaultAsync(a => a.NameAR.Equals(model.NameAR) || a.NameEN.Equals(model.NameEN) && a.IsEnabled);

                if (result is not null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0006",
                        ErrorType = ErrorType.Error
                    };
                }

                _dbContext.Set<CarType>().Add(new CarType
                {
                    NameAR = model.NameAR,
                    NameEN = model.NameEN,
                    DescriptionAR = model.DescriptionAR,
                    DescriptionEN = model.DescriptionEN,
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
                    ErrorCode = "CT-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }

        public async Task<CommitResult> UpdateCarTypeAsync(UpdateCarTypeViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0011",
                        ErrorType = ErrorType.Error
                    };
                }
                if (model.Id <= 0)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }

                CarType carType = await _dbContext.Set<CarType>().FindAsync(model.Id);

                if (carType is null)
                {
                    return new CommitResult
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0004",
                        ErrorType = ErrorType.Error
                    };
                }

                carType.NameAR = model.NameAR;
                carType.NameEN = model.NameEN;
                carType.DescriptionAR = model.DescriptionAR;
                carType.DescriptionEN = model.DescriptionEN;
                carType.IsEnabled = model.IsEnabled;

                _dbContext.Set<CarType>().Update(carType);
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
                    ErrorCode = "CT-X0002",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
