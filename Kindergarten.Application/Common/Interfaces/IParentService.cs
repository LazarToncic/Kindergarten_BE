using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Interfaces;

public interface IParentService
{
    Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, ParentChildRelationship parentChildRelationship, List<ParentRequestChildDto> children,CancellationToken cancellationToken);
    Task<GetParentRequestQueryResponseDto> GetParentRequests(GetParentRequestQueryDto dto, CancellationToken cancellationToken);
    Task<ParentRequestSingleResponseDto> GetParentRequest(Guid id, CancellationToken cancellationToken);
    Task ApproveParentRequestOnline(Guid parentRequestId, CancellationToken cancellationToken);
    Task ApproveParentRequestInPerson(Guid parentRequestId, CancellationToken cancellationToken);

    Task RemoveParentRoleForOrphanedAsync(IEnumerable<string> orphanedParentUserIds, CancellationToken cancellationToken);
    Task DeactivateParentAsync(IList<string> parentUserIds, string performedByUserId, CancellationToken cancellationToken);
    
    Task<Guid> GetParentIdWithUserId(string userId, CancellationToken cancellationToken);
}