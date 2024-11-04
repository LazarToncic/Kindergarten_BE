using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten_BE.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ApiBaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}