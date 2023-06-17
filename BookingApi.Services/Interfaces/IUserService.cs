using BookingApi.Services.Model.User;
using FluentResults;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BookingApi.Services.Interfaces;

public interface IUserService
{
    public Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel);
    public Task<Result<UserModel>> GetAsync(Guid id);
    public Task<Result> SignIn(SignInRequestModel signInRequestModel);
    public Task<PasswordVerificationResult> VerifyUserAsync(string emailAddress, string password);
    public Task<Result<UserModel>> UpdateAsync(UpdateUserRequestModel updateUserRequestModel);
}