using Kindergarten.Application.Employee.Commands;
using Kindergarten.Application.Employee.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class EmployeeController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpPost]
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> UpdateEmployeePosition([FromBody] UpdateEmployeePositionCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet] 
    [Authorize(Roles = "Coordinator,Owner,Manager")]
    public async Task<ActionResult> GetAllEmployeesForSingleKindergarten([FromQuery] GetAllEmployeesForSingleKindergartenQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    
}