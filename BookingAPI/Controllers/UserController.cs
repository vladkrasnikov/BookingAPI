using BookingApi.Data.Models;
using BookingAPI.Models.User;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BookingAPI.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CreateUserResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create(
        CreateUserRequest createUserRequest)
    {
        var result = await _userService.CreateAsync(createUserRequest.Adapt<CreateUserRequestModel>());
        return Ok(result.Value.Adapt<CreateUserResponse>());
    }
}