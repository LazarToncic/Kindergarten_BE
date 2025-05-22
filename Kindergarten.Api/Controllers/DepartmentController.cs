using Kindergarten.Application.Child.Commands;
using Kindergarten.Application.Department.Commands;
using Kindergarten.Application.Department.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class DepartmentController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Manager,Owner")]
    public async Task<ActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> GetDepartments([FromQuery] GetDepartmentsForUnassignedChildrenQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Coordinator,Manager,Owner")]
    public async Task<ActionResult> UnassignChildFromDepartment([FromBody] UnassignChildFromDepartmentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

}