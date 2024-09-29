namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.Users;
using WebApi.Services;
using WebApi.Utils;

[Authorize]
[ApiController]
[Route("user")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model, CookieUtils.GetIpAddress(Request));
        CookieUtils.SetTokenCookie(Response,response.RefreshToken);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = _userService.RefreshToken(refreshToken, CookieUtils.GetIpAddress(Request));
        CookieUtils.SetTokenCookie(Response, response.RefreshToken);
        return Ok(response);
    }
    
    [HttpPost("revoke-token")]
    public IActionResult RevokeToken(RevokeTokenRequest model)
    {
        var token = model.Token ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Token is required" });

        _userService.RevokeToken(token, CookieUtils.GetIpAddress(Request));
        return Ok(new { message = "Token revoked" });
    }
    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

}