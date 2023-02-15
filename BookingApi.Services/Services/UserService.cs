using BookingApi.Data.Interfaces.Repository;
using BookingApi.Data.Models;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace BookingApi.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuditRepository _auditRepository;

    public UserService(
        IUserRepository userRepository,
        IAuditRepository auditRepository)
    {
        _userRepository = userRepository;
        _auditRepository = auditRepository;
    }

    public async Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel)
    {
        var userModelHasher = new PasswordHasher<User>();
        var userEntity = createUserRequestModel.Adapt<User>();
        userEntity.Password = userModelHasher.HashPassword(userEntity, createUserRequestModel.Password);
        
        await _userRepository.CreateAsync(userEntity);
        await _auditRepository.CreateAsync($"User created: Id {userEntity.Id}, {userEntity.EmailAddress}", new Audit(), userEntity);
        
        return Result.Ok(userEntity.Adapt<UserModel>());
    }

    public async Task<PasswordVerificationResult> VerityAsync(string emailAddress, string password)
    {
        var user = await _userRepository.GetAsync(emailAddress);
        var userHasher = new PasswordHasher<User>();
        var verificationResult = userHasher.VerifyHashedPassword(user, user.Password, password);
        return verificationResult;
    }
}