using BookingApi.Services.Model.User;
using FluentResults;

namespace BookingApi.Services.Interfaces;

public interface IUserService
{
    public Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel);
}