using Kindergarten.Application.Department.Commands;
using Kindergarten.Application.Department.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class DepartmentController : ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetDepartments([FromQuery] GetDepartmentsForUnassignedChildrenQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

}