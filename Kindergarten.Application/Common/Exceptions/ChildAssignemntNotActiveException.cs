namespace Kindergarten.Application.Common.Exceptions;

public class ChildAssignemntNotActiveException : BaseException
{
    public ChildAssignemntNotActiveException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}