using Kindergarten.Application.Common.Dto.Children;

namespace Kindergarten.Application.Common.Interfaces;

public interface IMedicalConditionService
{ 
    public Task CreateMedicalConditiionsForChildrenThroughParentRequest(List<NewChildrenThroughParentRequestDto> children, CancellationToken cancellationToken);
}