namespace Kindergarten.Application.Common.Exceptions;

public class PermissionDeniedException : BaseException
{
    public PermissionDeniedException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}