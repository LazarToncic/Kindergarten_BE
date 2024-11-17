using Kindergarten.Application.Common.Dto.Department;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Department;
using Kindergarten.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Department.Commands;

public record CreateDepartmentCommand(CreateDepartmentDto Dto) : IRequest;

public class CreateDepartmentCommandHandler(IKindergartenDbContext dbContext) : IRequestHandler<CreateDepartmentCommand>
{
    public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var kindergarten = await dbContext.Kindergartens
            .Where(x => x.Name.Equals(request.Dto.KindergartenName))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (kindergarten == null)
            throw new NotFoundException("Kindergarten with this name doesnt exist.",
                new {request.Dto.KindergartenName});
        
        var department = request.Dto.FromCreateDepartmentDtoToDepartment();
        
        var existingDepartment = await dbContext.Departments
            .Where(x => x.Name == department.Name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingDepartment != null)
        {
            throw new ConflictException("Department with this name already exists.",
                new {existingDepartment.Name});
        }
        
        dbContext.Departments.Add(department);
        await dbContext.SaveChangesAsync(cancellationToken);

        var kindergartenDepartment = new KindergartenDepartment
        {
            DepartmentId = department.Id,
            KindergartenId = kindergarten.Id
        };

        dbContext.KindergartenDepartments.Add(kindergartenDepartment);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
} 