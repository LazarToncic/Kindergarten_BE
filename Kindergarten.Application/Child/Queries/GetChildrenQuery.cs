using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Child.Queries;

public record GetChildrenQuery(GetChildrenQueryDto? Dto) : IRequest<GetChildrenQueryResponseList>;

public class GetChildrenQueryHandler(IChildrenService childrenService) : IRequestHandler<GetChildrenQuery, GetChildrenQueryResponseList>
{
    public async Task<GetChildrenQueryResponseList> Handle(GetChildrenQuery request, CancellationToken cancellationToken)
    {
        return await childrenService.GetChildren(
            request.Dto?.KindergartenId, 
            request.Dto?.FirstName,
            request.Dto?.LastName,
            request.Dto?.BirthDate,
            request.Dto?.IsActive,
            cancellationToken
            ); 
    }
}