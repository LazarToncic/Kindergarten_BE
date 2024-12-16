namespace Kindergarten.Application.Common.Exceptions;

public class CoordinatorException : BaseException
{
    public CoordinatorException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}