using Kindergarten.Application.Common.Dto.Qualification;
using Kindergarten.Application.Common.Interfaces;

namespace Kindergarten.Infrastructure.Services;

public class QualificationService : IQualificationService
{
    public Task<bool> AssignQualificationToNewEmployee(List<QualificationCreateEmployeeDto> qualifications)
    {
        throw new NotImplementedException();
    }
}