namespace Kindergarten.Application.Common.Dto.Parent;

public record GetParentRequestQueryDto(string? FirstName, string? LastName, string KindergartenName, bool? IsOnlineApproved,
    bool? IsInPersonApproved, int PageNumber = 1, int PageSize = 10);