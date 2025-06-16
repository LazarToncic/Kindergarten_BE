using Kindergarten.Application.Kindergarten.Commands;
using Kindergarten.Application.Kindergarten.Queries;
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

    [Authorize(Roles = "Owner,Parent,Coordinator,Manager")]
    [HttpGet]
    public async Task<ActionResult> GetKindergartensInf([FromQuery] GetKindergartensInfQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}