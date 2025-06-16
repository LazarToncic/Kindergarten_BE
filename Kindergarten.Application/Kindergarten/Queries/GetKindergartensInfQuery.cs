using Kindergarten.Application.Common.Dto.Kindergarten;
using Kindergarten.Application.Common.Interfaces;
using MediatR;

namespace Kindergarten.Application.Kindergarten.Queries;

public record GetKindergartensInfQuery() : IRequest<List<GetIdAndNameKindergartenDto>>;

public class GetKindergartensInfQueryHandler(IKindergartenService kindergartenService) : IRequestHandler<GetKindergartensInfQuery, List<GetIdAndNameKindergartenDto>>
{
    public async Task<List<GetIdAndNameKindergartenDto>> Handle(GetKindergartensInfQuery request, CancellationToken cancellationToken)
    {
        return await kindergartenService.GetKindergartenIdsAndNames();
    }
}