namespace Kindergarten.Application.Common.Interfaces;

public interface IDepartmentService
{
    public Task<bool> AssignNewEmployeeToDepartment(string nameOfDepartment, Guid newEmployeeId, CancellationToken cancellationToken);
}