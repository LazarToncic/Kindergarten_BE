using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;

public record GetChildWithdrawalRequestsQueryDto(Guid? KindergartenId, string? FirstName, string? LastName, ChildWithdrawalRequestStatus? Status);