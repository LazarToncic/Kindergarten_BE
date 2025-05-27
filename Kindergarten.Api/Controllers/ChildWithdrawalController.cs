using Kindergarten.Application.ChildWithdrawal.Commands;
using Kindergarten.Application.ChildWithdrawal.Queries;
using Kindergarten.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class ChildWithdrawalController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Employee,Coordinator,Manager,Owner")]
    public async Task<ActionResult> CreateChildWithdrawalRequest([FromBody] ChildWithdrawalRequestCommand request)
    {
        await Mediator.Send(request);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> GetChildWithdrawalRequests([FromQuery] GetChildWithdrawalRequestsQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> RejectChildWithdrawalRequest([FromBody] RejectChildWithdrawalCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> ApproveChildWithdrawalRequest([FromBody] ApproveChildWithdrawalCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

}