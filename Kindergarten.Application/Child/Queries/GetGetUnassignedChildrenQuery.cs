using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Child.Queries;

public record GetGetUnassignedChildrenQuery(GetGetUnassignedChildrenQueryDto Dto) : IRequest<GetUnassignedChildrenDto>;

public class GetGetUnassignedChildrenQueryHandler(IChildrenService childrenService) : IRequestHandler<GetGetUnassignedChildrenQuery, GetUnassignedChildrenDto>
{
    public async Task<GetUnassignedChildrenDto> Handle(GetGetUnassignedChildrenQuery request, CancellationToken cancellationToken)
    {
        return await childrenService.GetUnassignedChildren(request.Dto.KindergartenId,
            request.Dto.FirstName,
            request.Dto.LastName,
            cancellationToken);
    }
}