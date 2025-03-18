using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Interfaces;

public interface IParentService
{
    Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, ParentChildRelationship parentChildRelationship, List<ParentRequestChildDto> children,CancellationToken cancellationToken);
    Task<GetParentRequestQueryResponseDto> GetParentRequest(GetParentRequestQueryDto dto, CancellationToken cancellationToken);
    Task ApproveParentRequestOnline(Guid parentRequestId, CancellationToken cancellationToken);
    Task ApproveParentRequestInPerson(Guid parentRequestId, CancellationToken cancellationToken);
}