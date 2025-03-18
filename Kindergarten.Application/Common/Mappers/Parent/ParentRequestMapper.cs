using System.Text.Json;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Parent;

[Mapper]
public static partial class ParentRequestMapper
{
    public static ParentRequest NewParentRequest(string userId, int numberOfChildren, string? additionalInfo, 
        string preferredKindergarten, ParentChildRelationship parentChildRelationship, List<ParentRequestChildDto> children)
    {
        return new ParentRequest
        {
            UserId = userId,
            NumberOfChildren = numberOfChildren,
            AdditionalInfo = additionalInfo,
            IsOnlineApproved = false,
            IsInPersonApproved = false,
            PreferredKindergarten = preferredKindergarten,
            ParentRole = parentChildRelationship,
            ChildrenJson = JsonSerializer.Serialize(children)
        };
    }
}