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
    private PasswordHasher<UserModel> _userModelHasher;
    private readonly IUserRepository _userRepository;

    public UserService(PasswordHasher<UserModel> userModelHasher, IUserRepository userRepository)
    {
        _userModelHasher = userModelHasher;
        _userRepository = userRepository;
    }

    public async Task<Result<UserModel>> CreateAsync(CreateUserRequestModel createUserRequestModel)
    {
        var userEntity = createUserRequestModel.Adapt<User>();
        userEntity.Password = _userModelHasher.HashPassword(new UserModel(), createUserRequestModel.Password);
        await _userRepository.CreateAsync(userEntity);
        //add record to audit table
        
        return Result.Ok(userEntity.Adapt<UserModel>());
    }
}