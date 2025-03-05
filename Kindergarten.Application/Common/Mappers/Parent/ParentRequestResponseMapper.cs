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
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var parentRequestDtos = parentRequests.Select(pr => new ParentRequestSingleResponseDto(
            pr.Id,
            pr.User.FirstName,
            pr.User.LastName,
            pr.User.Email!,
            pr.User.PhoneNumber!,
            pr.PreferredKindergarten,
            pr.IsOnlineApproved,
            pr.IsInPersonApproved,
            pr.CreatedAt,
            JsonSerializer.Deserialize<List<ParentRequestChildDto>>(pr.ChildrenJson, options) 
            ?? new List<ParentRequestChildDto>(),
            pr.OnlineApprovedByUser != null 
                ? $"{pr.OnlineApprovedByUser.FirstName} {pr.OnlineApprovedByUser.LastName}" 
                : "Not approved yet",
            pr.InPersonApprovedByUser != null 
                ? $"{pr.InPersonApprovedByUser.FirstName} {pr.InPersonApprovedByUser.LastName}" 
                : "Not approved yet"
        )).ToList();

        return new GetParentRequestQueryResponseDto(parentRequestDtos);
    }
}