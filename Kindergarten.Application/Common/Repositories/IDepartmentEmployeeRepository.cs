namespace Kindergarten.Application.Common.Repositories;

public interface IDepartmentEmployeeRepository
{
    Task<bool> IsTeacherInDepartmentAsync(Guid departmentId, string userId, Guid kindergartenId ,CancellationToken ct);
}