namespace Kindergarten.Application.Common.Exceptions;

public class UnableToDeleteRoleException : BaseException
{
    public UnableToDeleteRoleException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}