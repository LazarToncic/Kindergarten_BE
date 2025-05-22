using Kindergarten.Application.Child.Commands;
using Kindergarten.Application.Child.Queries;
using Kindergarten.Application.Department.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class ChildController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Parent")]
    public async Task<ActionResult> EnrollNewChild([FromBody] EnrollNewChildCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> GetGetUnassignedChildren([FromQuery] GetGetUnassignedChildrenQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> AssignChildToDepartment([FromBody] AssignChildToDepartmentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> UnassignChildFromDepartment([FromBody] UnassignChildFromDepartmentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> GetChildren([FromQuery] GetChildrenQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}