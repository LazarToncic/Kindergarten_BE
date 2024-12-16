using Kindergarten.Application.EmployeePositions.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class EmployeePositionsController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Owner,Coordinator")]
    public async Task<ActionResult> CreateEmployeePositions([FromBody] CreateEmployeePositionsCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}