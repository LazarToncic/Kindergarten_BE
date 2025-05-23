using Kindergarten.Application.ChildWithdrawal.Commands;
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
}