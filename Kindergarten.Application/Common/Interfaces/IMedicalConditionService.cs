using Kindergarten.Application.Common.Dto.Children;

namespace Kindergarten.Application.Common.Interfaces;

public interface IMedicalConditionService
{ 
    public Task CreateMedicalConditiionsForChildrenThroughParentRequest(List<NewChildrenThroughParentRequestDto> children, CancellationToken cancellationToken);
    public Task CreateMedicalConditionsForNewChild(Guid childId, bool hasMedicalIssues,
        List<string>? medicalConditions, CancellationToken cancellationToken );
}