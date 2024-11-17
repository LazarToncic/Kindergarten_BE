using Kindergarten.Application.Department.Commands;
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
}