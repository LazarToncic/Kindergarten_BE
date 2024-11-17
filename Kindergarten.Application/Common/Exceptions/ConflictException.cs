namespace Kindergarten.Application.Common.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}