using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.EmployeePositions.Commands;

public record CreateEmployeePositionsCommand(string Name) : IRequest;

public class CreateEmployeePositionsCommandHandler(IKindergartenDbContext dbContext) : IRequestHandler<CreateEmployeePositionsCommand>
{
    public async Task Handle(CreateEmployeePositionsCommand request, CancellationToken cancellationToken)
    {
        var employeePosition = await dbContext.EmployeePositions
            .Where(x => x.Name.Equals(request.Name))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (employeePosition != null)
            throw new ConflictException("EmployeePosition with this name already exists",
                new {request.Name});

        var newEmployeePosition = new EmployeePosition
        {
            Name = request.Name
        };
        
        dbContext.EmployeePositions.Add(newEmployeePosition);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 