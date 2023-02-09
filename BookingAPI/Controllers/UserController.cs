using BookingAPI.Models.User;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Endpoint to create a new user
    /// </summary>
    /// <param name="createUserRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        CreateUserRequest createUserRequest)
    {
        var result = await _userService.CreateAsync(createUserRequest.Adapt<CreateUserRequestModel>());
        return Ok(result.Value.Adapt<CreateUserResponse>());
    }
}