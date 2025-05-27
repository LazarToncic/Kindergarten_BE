using Kindergarten.Application.Common.Dto.ChildWithdrawalRequest;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using MediatR;

namespace Kindergarten.Application.ChildWithdrawal.Queries;

public record GetChildWithdrawalRequestsQuery(GetChildWithdrawalRequestsQueryDto? Dto) : IRequest<WithdrawalRequestDtoResponseList>;

public class GetChildWithdrawalRequestsQueryHandler(IChildWithdrawalService childWithdrawalService) : IRequestHandler<GetChildWithdrawalRequestsQuery, WithdrawalRequestDtoResponseList>
{
    public async Task<WithdrawalRequestDtoResponseList> Handle(GetChildWithdrawalRequestsQuery request, CancellationToken cancellationToken)
    {
        return await childWithdrawalService.GetChildWithdrawalsAsync(
            request.Dto?.KindergartenId,
            request.Dto?.FirstName,
            request.Dto?.LastName,
            request.Dto?.Status,
            cancellationToken
            ); 
    }
}