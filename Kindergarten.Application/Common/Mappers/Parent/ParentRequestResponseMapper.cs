using System.Text.Json;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Parent;

[Mapper]
public static partial class ParentRequestResponseMapper
{
    public static GetParentRequestQueryResponseDto ParentRequestToGetParentRequestQueryResponseDto(this List<ParentRequest> parentRequests)
    {
        var parentRequestDtos = parentRequests.Select(pr => new ParentRequestSingleResponseDto(
            pr.Id,
            pr.User.FirstName,
            pr.User.LastName,
            pr.PreferredKindergarten,
            pr.IsOnlineApproved,
            pr.IsInPersonApproved,
            pr.CreatedAt,
            JsonSerializer.Deserialize<List<ParentRequestChildDto>>(pr.ChildrenJson)!
        )).ToList();

        return new GetParentRequestQueryResponseDto(parentRequestDtos);
    }
}