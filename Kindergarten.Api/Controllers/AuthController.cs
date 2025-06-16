using Kindergarten.Application.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class AuthController : ApiBaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> UserRegistration([FromBody] UserRegistrationCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> UserLogin(UserLoginCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UserLogout(UserLogoutCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> GenerateRefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}