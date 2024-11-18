using Kindergarten.Application.QualificationType.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

public class QualificationTypeController : ApiBaseController
{
    [HttpPost]
    [Authorize(Roles = "Coordinator,Owner")]
    public async Task<ActionResult> CreateQualificationType(QualificationTypeCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}