namespace Kindergarten.Application.Common.Exceptions;

public class ParentRequestOnlineNotApprovedException : BaseException
{
    public ParentRequestOnlineNotApprovedException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}