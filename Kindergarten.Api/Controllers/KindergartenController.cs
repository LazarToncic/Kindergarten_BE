using Kindergarten.Application.Kindergarten.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class KindergartenController : ApiBaseController
{
    [Authorize(Roles = "Owner")]
    [HttpPost]
    public async Task<ActionResult> CreateKindergarten( [FromBody] CreateKindergartenCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}