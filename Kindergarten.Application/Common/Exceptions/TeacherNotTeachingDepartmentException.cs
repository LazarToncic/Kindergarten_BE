namespace Kindergarten.Application.Common.Exceptions;

public class TeacherNotTeachingDepartmentException : BaseException
{
    public TeacherNotTeachingDepartmentException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }
}