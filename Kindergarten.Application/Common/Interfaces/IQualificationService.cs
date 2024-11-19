using Kindergarten.Application.Common.Dto.Qualification;

namespace Kindergarten.Application.Common.Interfaces;

public interface IQualificationService
{
    public Task<bool> AssignQualificationToNewEmployee(List<QualificationCreateEmployeeDto> qualifications, Guid employeeId, CancellationToken cancellationToken);

    public string GetStrongestQualificationWhenCreatingNewEmployee(
        List<string> qualifications);
}