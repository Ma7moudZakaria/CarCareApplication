using CarCareApplication.Core.Shared.ErrorHandler;
using CarCareApplication.Core.Shared.Models;
using CarCareApplication.Core.Shared.ViewModels.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace CarCareApplication.Core.Shared.Repositories
{
    public class UserRepo
    {
        private CarCareApplicationDbContext _dbContext;
        public UserRepo(CarCareApplicationDbContext db)
        {
            _dbContext = db;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<CommitResult<User>> SigninAsync(SigninUserViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0001",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                User user = await _dbContext.Set<User>().SingleOrDefaultAsync(a => a.PhoneNumber.Equals(model.PhoneNumber) && a.Password.Equals(model.Password));

                if (user == null)
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                await _dbContext.Entry(user).Reference(a => a.Role).LoadAsync();
                return new CommitResult<User>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = user
                };
            }
            catch
            {
                return new CommitResult<User>
                {
                    IsSuccess = false,
                    ErrorCode = "UR-X0005",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<User>> SignupAsync(SignupUserViewModel model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0009",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.FirstName))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0006",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.SecondName))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0007",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                if (model.RoleId <= 0)
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0010",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                User user = await _dbContext.Set<User>().SingleOrDefaultAsync(a => a.PhoneNumber.Equals(model.PhoneNumber));

                if (user is not null)
                {
                    return new CommitResult<User>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0008",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }
                EntityEntry<User> createdUser = _dbContext.Set<User>().Add(new User
                {
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    RoleId = model.RoleId,
                });
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(createdUser.Entity).Reference(a => a.Role).LoadAsync();
                return new CommitResult<User>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = createdUser.Entity
                };
            }
            catch
            {
                return new CommitResult<User>
                {
                    IsSuccess = false,
                    ErrorCode = "UR-X0005",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }
    }
}
