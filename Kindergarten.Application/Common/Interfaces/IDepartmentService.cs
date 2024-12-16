namespace Kindergarten.Application.Common.Interfaces;

public interface IDepartmentService
{
    public Task<bool> AssignNewEmployeeToDepartment(string nameOfDepartment, string positionInDepartment, Guid newEmployeeId, string strongestQualification, CancellationToken cancellationToken);

    public Task<bool> CheckIfEmployeeCanWorkWithAgeGroup(string nameOfDepartment, string strongestQualification);
}