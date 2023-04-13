using System.Security.Claims;
using BookingApi.Data.Interfaces.Repository;
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
    private readonly IUserRepository _userRepository;
    private readonly IAuditRepository _auditRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IAuditRepository auditRepository,
        IHttpContextAccessor httpContextAccessor,
        IPasswordHasher<UserModel> passwordHasher)
    {
        _userRepository = userRepository;
        _auditRepository = auditRepository; 
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel)
    {
        var userModel = createUserRequestModel.Adapt<UserModel>();
        userModel.Password = _passwordHasher.HashPassword(userModel, createUserRequestModel.Password);

        var userEntity = userModel.Adapt<User>();
        await _userRepository.CreateAsync(userEntity);
        await _auditRepository.CreateAsync($"User created: Id {userModel.Id}, {userModel.EmailAddress}", new Audit(),
            userEntity);

        return Result.Ok(userModel.Adapt<UserModel>());
    }

    public async Task<Result<UserModel>> GetAsync(Guid id)
    {
        var userResult = await _userRepository.GetAsync(id);
        if (userResult.IsFailed)
            return Result.Fail<UserModel>($"User with id {id} not found");

        return Result.Ok(userResult.Value.Adapt<UserModel>());
    }

    public async Task<Result> SignIn(SignInRequestModel signInRequestModel)
    {
        var userResult = await _userRepository.GetAsync(signInRequestModel.EmailAddress);

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
            new(ClaimTypes.Email, signInRequestModel.EmailAddress),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Expired, user.Blocked.ToString()),
            new(ClaimTypes.Role, "User")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
            AllowRefresh = true
        };

        authProperties.StoreTokens(new[]
        {
            new AuthenticationToken
            {
                Name = "localhost:3000"
            }
        });

        await (_httpContextAccessor.HttpContext ?? throw new InvalidOperationException()).SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            authProperties);

        return Result.Ok();
    }

    private Task<PasswordVerificationResult> VerifyAsync(UserModel user, string password)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return Task.FromResult(verificationResult);
    }

    public async Task<PasswordVerificationResult> VerifyUserAsync(string emailAddress, string password)
    {
        var userResult = await _userRepository.GetAsync(emailAddress);
        if (userResult.IsFailed)
            return PasswordVerificationResult.Failed;

        var userModel = userResult.Value.Adapt<UserModel>();
        var verificationResult = _passwordHasher.VerifyHashedPassword(userModel, userModel.Password, password);
        return verificationResult;
    }
}