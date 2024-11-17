using Kindergarten.Application.Common.Dto.Kindergarten;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Kindergarten;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Kindergarten.Commands;

public record CreateKindergartenCommand(CreateKindergartenDto Dto) : IRequest;

public class CreateKindergartenCommandHandler(IKindergartenDbContext dbContext) : IRequestHandler<CreateKindergartenCommand>
{
    public async Task Handle(CreateKindergartenCommand request, CancellationToken cancellationToken)
    {
        var checkExistingKindergarten = await dbContext.Kindergartens
            .Where(x => request.Dto.Name == x.Name ||
                        (request.Dto.Address == x.Address && request.Dto.City == x.City))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (checkExistingKindergarten != null)
            throw new Exception("This entity already exists");

        dbContext.Kindergartens.Add(request.Dto.FromCreateKindergartenDtoToKindergarten());
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 