using Kindergarten.Application.Common.Dto.Parent;

namespace Kindergarten.Application.Common.Interfaces;

public interface IParentService
{
    Task CreateParentRequest(int numberOfChildren, string? additionalInfo, string preferredKindergarten, List<ParentRequestChildDto> children,CancellationToken cancellationToken);
    Task<GetParentRequestQueryResponseDto> GetParentRequest(GetParentRequestQueryDto dto, CancellationToken cancellationToken);
}