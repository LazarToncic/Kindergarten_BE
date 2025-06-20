using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Parent.Queries;

public record GetParentRequestQuery(Guid Id) : IRequest<ParentRequestSingleResponseDto>;

public class GetParentRequestQueryHandler(IParentService parentService) : IRequestHandler<GetParentRequestQuery, ParentRequestSingleResponseDto>
{
    public async Task<ParentRequestSingleResponseDto> Handle(GetParentRequestQuery request, CancellationToken cancellationToken)
    {
        return await parentService.GetParentRequest(request.Id, cancellationToken);
    }
}