using Kindergarten.Application.Parent.Commands;
using Kindergarten.Application.Parent.Queries;
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
    
    [HttpGet]
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> GetParentRequests([FromQuery] GetParentRequestQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> ApproveParentRequestOnline([FromQuery] ApproveParentRequestOnlineCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> ApproveParentRequestInPerson([FromQuery] ApproveParentRequestInPersonCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}