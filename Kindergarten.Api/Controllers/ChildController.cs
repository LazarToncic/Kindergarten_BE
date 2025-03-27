using Kindergarten.Application.Child.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class ChildController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Parent")]
    public async Task<ActionResult> EnrollNewChild([FromQuery] EnrollNewChildCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}