using System.Security.Claims;
using Azure;
using BookingAPI.Models.User;
using BookingApi.Services.Interfaces;
using BookingApi.Services.Model.User;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.Controllers;

public class AuthController : Controller
{
    private readonly IUserService _userService;
    
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
    {
        var signInModel = signInRequest.Adapt<SignInRequestModel>();
        return Ok(await _userService.SignIn(signInModel));
    }
    
    [Authorize]
    [HttpGet("user")]
    public IActionResult GetUser()
    {
        var userClaims = User.Claims.Select(x => new UserClaim(x.Type, x.Value)).ToList();

        return Ok(userClaims);
    }
    
    [Authorize]
    [HttpGet("signout")]
    public async Task SignOutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

}