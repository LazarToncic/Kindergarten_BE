using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;

namespace Kindergarten.Application.Common.Interfaces;

public interface IAllergyService
{
    public Task<Guid> CreateAllergy(string allergy, CancellationToken cancellationToken);
    public Task CreateAllergiesForChildrenThroughParentRequest(List<NewChildrenThroughParentRequestDto> children, CancellationToken cancellationToken);
}