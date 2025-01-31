using Kindergarten.Application.Common.Dto.Qualification;

namespace Kindergarten.Application.Common.Interfaces;

public interface ISalaryService
{
    public Task<string> CreateSalaryForNewEmployee(List<QualificationCreateEmployeeDto> qualifications, string employeePositionName, Guid employeeId,
        CancellationToken cancellationToken);
    
    public Task CreateNewSalaryWhenEmployeeIsChangingPositions(Guid employeeId,Guid employeePositionId, CancellationToken cancellationToken);
}