using BookingApi.Services.Model.User;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace BookingApi.Services.Interfaces;

public interface IUserService
{
    public Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel);
    public Task<PasswordVerificationResult> VerityAsync(string emailAddress, string password);
}