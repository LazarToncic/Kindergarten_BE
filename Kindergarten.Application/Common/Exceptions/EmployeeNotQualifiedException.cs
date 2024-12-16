namespace Kindergarten.Application.Common.Exceptions;

public class EmployeeNotQualifiedException : BaseException
{
    public EmployeeNotQualifiedException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}