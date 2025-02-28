using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Queries;

public record GetParentRequestQuery(GetParentRequestQueryDto Dto) : IRequest<GetParentRequestQueryResponseDto>;

public class GetParentRequestQueryHandler(IParentService parentService) : IRequestHandler<GetParentRequestQuery, GetParentRequestQueryResponseDto>
{
    public async Task<GetParentRequestQueryResponseDto> Handle(GetParentRequestQuery request, CancellationToken cancellationToken)
    {
        return await parentService.GetParentRequest(request.Dto, cancellationToken);
    }
} 