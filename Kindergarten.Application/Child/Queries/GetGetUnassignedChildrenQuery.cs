using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Child.Queries;

public record GetGetUnassignedChildrenQuery(GetGetUnassignedChildrenQueryDto Dto) : IRequest<GetUnassignedChildrenListDto>;

public class GetGetUnassignedChildrenQueryHandler(IChildrenService childrenService) : IRequestHandler<GetGetUnassignedChildrenQuery, GetUnassignedChildrenListDto>
{
    public async Task<GetUnassignedChildrenListDto> Handle(GetGetUnassignedChildrenQuery request, CancellationToken cancellationToken)
    {
        return await childrenService.GetUnassignedChildren(request.Dto.KindergartenId,
            request.Dto.FirstName,
            request.Dto.LastName,
            cancellationToken);
    }
}