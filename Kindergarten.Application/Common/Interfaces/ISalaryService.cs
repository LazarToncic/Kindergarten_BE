using Kindergarten.Application.Common.Dto.Qualification;

namespace Kindergarten.Application.Common.Interfaces;

public interface ISalaryService
{
    public Task<bool> CreateSalaryForNewEmployee(List<QualificationCreateEmployeeDto> qualifications, string employeePositionName, Guid employeeId,
        CancellationToken cancellationToken);
}