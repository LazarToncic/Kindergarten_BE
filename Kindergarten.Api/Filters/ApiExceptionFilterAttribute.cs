using Kindergarten.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kindergarten_BE.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            {typeof(ValidationException), HandleValidationException},
            {typeof(FluentValidation.ValidationException), HandleFluentValidationException},
            {typeof(NotFoundException), HandleNotFoundException},
            {typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException},
            {typeof(ConflictException), HandleConflictException},
            {typeof(EmployeeNotQualifiedException), HandleEmployeeNotQualifiedException},
            {typeof(ChildAssignemntNotActiveException), HandleChildAssignemntNotActiveException},
            {typeof(TeacherNotTeachingDepartmentException), HandleTeacherNotTeachingDepartmentException},
            {typeof(UnableToDeleteRoleException), HandleUnableToDeleteRoleException},
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        if (_exceptionHandlers.TryGetValue(type, out var handler))
        {
            handler.Invoke(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unknown Exception",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status409Conflict
        };
        
        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
        
        context.ExceptionHandled = true;
    }
    
    private void HandleEmployeeNotQualifiedException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
        
        context.ExceptionHandled = true;
    }
    
    private void HandleUnableToDeleteRoleException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status406NotAcceptable,
            Title = "Unable to delete role",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status406NotAcceptable
        };
        
        context.ExceptionHandled = true;
    }
    
    private void HandleTeacherNotTeachingDepartmentException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
        
        context.ExceptionHandled = true;
    }
    
    private void HandleChildAssignemntNotActiveException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7231#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status406NotAcceptable
        };
        
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found",
            Detail = exception.Message
        };
        
        if (exception.AdditionalData != null)
        {
            details.Extensions.Add("additionalData", exception.AdditionalData);
        }

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;

    }

    private void HandleFluentValidationException(ExceptionContext context)
    {
        throw new NotImplementedException();
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ValidationProblemDetails((IDictionary<string, string[]>)exception.AdditionalData!)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleConflictException(ExceptionContext context)
    {
        var exception = (ConflictException)context.Exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource already exists",
            Detail = exception.Message
        };
        
        if (exception.AdditionalData != null)
        {
            details.Extensions.Add("additionalData", exception.AdditionalData);
        }

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
}