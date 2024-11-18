using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.QualificationType;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.QualificationType.Commands;

public record QualificationTypeCommand(string Name) : IRequest;

public class QualificationTypeCommandHandler(IKindergartenDbContext dbContext) : IRequestHandler<QualificationTypeCommand>
{
    public async Task Handle(QualificationTypeCommand request, CancellationToken cancellationToken)
    {
        var qualificationType = await dbContext.QualificationTypes
            .Where(x => x.Name.Equals(request.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (qualificationType != null)
            throw new ConflictException("Qualification Type with this name already exists.",
            new {request.Name});

        var newQualificationType = new Domain.Entities.QualificationType
        {
            Name = request.Name
        };
        
        dbContext.QualificationTypes.Add(newQualificationType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 