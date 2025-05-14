using Kindergarten.Application.Common.Dto.Department;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Department;
using Kindergarten.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Department.Commands;

public record CreateDepartmentCommand(CreateDepartmentDto Dto) : IRequest;

public class CreateDepartmentCommandHandler(IDepartmentService departmentService) : IRequestHandler<CreateDepartmentCommand>
{
    public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        await departmentService.CreateDepartment(
            request.Dto.KindergartenId, 
            request.Dto.AgeGroup, 
            request.Dto.MaximumCapacity,
            request.Dto.Name,
            cancellationToken
            );
    }
} 