using System.Text.Json.Serialization;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Application.Common.Dto.Parent;

public record ParentRequestSingleResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string KindergartenName,
    bool IsOnlineApproved,
    bool IsInPersonApproved,
    DateTime CreatedAt,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    ParentChildRelationship ParentChildRelationship,
    List<ParentRequestChildDto>? ChildrenJson,
    string? OnlineApprovedBy,   
    string? InPersonApprovedBy
    );