using Kindergarten.Application.Common.Dto.Qualification;

namespace Kindergarten.Application.Common.Interfaces;

public interface IQualificationService
{
    public Task<bool> AssignQualificationToNewEmployee(List<QualificationCreateEmployeeDto> qualifications);
}