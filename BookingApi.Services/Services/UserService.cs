using System.Security.Claims;
using BookingApi.Data.Helpers.Interfaces;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BookingApi.Services.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserService(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IPasswordHasher<UserModel> passwordHasher)
    {
        _unitOfWork = unitOfWork; 
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel)
    {
        var userModel = createUserRequestModel.Adapt<UserModel>();
        userModel.Password = _passwordHasher.HashPassword(userModel, createUserRequestModel.Password);

        var userEntity = userModel.Adapt<User>();
        await _unitOfWork.User.CreateAsync(userEntity);
        await _unitOfWork.Audit.CreateAsync($"User created: Id {userModel.Id}, {userModel.EmailAddress}", new Audit(),
            userEntity);

        await _unitOfWork.SaveAsync();

        return Result.Ok(userEntity.Adapt<UserModel>());
    }

    public async Task<Result<UserModel>> GetAsync(Guid id)
    {
        var userResult = await _unitOfWork.User.GetAsync(id);
        if (userResult.IsFailed)
            return Result.Fail<UserModel>($"User with id {id} not found");

        return Result.Ok(userResult.Value.Adapt<UserModel>());
    }

    public async Task<Result> SignIn(SignInRequestModel signInRequestModel)
    {
        var userResult = await _unitOfWork.User.GetAsync(signInRequestModel.EmailAddress);

        if (userResult.IsFailed)
            return Result.Fail($"User with email address {signInRequestModel.EmailAddress} not found");

        var user = userResult.Value.Adapt<UserModel>();

        var verificationResult = await VerifyAsync(user, signInRequestModel.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Result.Fail("Invalid password");
        }

        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.UserData, signInRequestModel.EmailAddress),
            new(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Expired, user.Blocked.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await (_httpContextAccessor.HttpContext ?? throw new InvalidOperationException()).SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            });

        return Result.Ok();
    }

    private Task<PasswordVerificationResult> VerifyAsync(UserModel user, string password)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return Task.FromResult(verificationResult);
    }

    public async Task<PasswordVerificationResult> VerifyUserAsync(string emailAddress, string password)
    {
        var userResult = await _unitOfWork.User.GetAsync(emailAddress);
        if (userResult.IsFailed)
            return PasswordVerificationResult.Failed;

        var userModel = userResult.Value.Adapt<UserModel>();
        var verificationResult = _passwordHasher.VerifyHashedPassword(userModel, userModel.Password, password);
        return verificationResult;
    }

    public async Task<Result<UserModel>> UpdateAsync(UpdateUserRequestModel updateUserRequestModel)
    {
        var userResult = await _unitOfWork.User.GetAsync(updateUserRequestModel.Id);
        if (userResult.IsFailed)
            return Result.Fail($"User with id {updateUserRequestModel.Id} not found");
        
        var user = userResult.Value;
        user.FirstName = updateUserRequestModel.FirstName ?? user.FirstName;
        user.LastName = updateUserRequestModel.LastName ?? user.LastName;
        user.Image = updateUserRequestModel.Image ?? user.Image;
        
        _unitOfWork.User.UpdateAsync(user);
        
        await _unitOfWork.Audit.CreateAsync($"User updated: Id {user.Id}, {user.EmailAddress}", new Audit(),
            user);
        
        await _unitOfWork.SaveAsync();
        
        return Result.Ok(user.Adapt<UserModel>());
    }
}