using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Queries;

public record GetParentRequestsQuery(GetParentRequestQueryDto Dto) : IRequest<GetParentRequestQueryResponseDto>;

public class GetParentRequestsQueryHandler(IParentService parentService) : IRequestHandler<GetParentRequestsQuery, GetParentRequestQueryResponseDto>
{
    public async Task<GetParentRequestQueryResponseDto> Handle(GetParentRequestsQuery request, CancellationToken cancellationToken)
    {
        return await parentService.GetParentRequests(request.Dto, cancellationToken);
    }
} 