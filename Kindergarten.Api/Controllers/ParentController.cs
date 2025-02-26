using Kindergarten.Application.Parent.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class ParentController : ApiBaseController
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> SendParentRequest([FromBody] SendParentRequestCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}