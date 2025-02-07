using Kindergarten.Application.Common.Dto.Employee;
using Kindergarten.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.Employee.Queries;

public record GetAllEmployeesForSingleKindergartenQuery(Guid KindergartenId, string? Email, string? FirstName, string? LastName)
    : IRequest<GetAllEmployeesForSingleKindergartenQueryResponseDto>;

public class GetAllEmployeesForSingleKindergartenQueryHandler(IKindergartenDbContext dbContext) : IRequestHandler<GetAllEmployeesForSingleKindergartenQuery, GetAllEmployeesForSingleKindergartenQueryResponseDto>
{
    public async Task<GetAllEmployeesForSingleKindergartenQueryResponseDto> Handle(
        GetAllEmployeesForSingleKindergartenQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Employees
            .Include(u => u.User)
            .Include(ep => ep.EmployeePosition)
            .Where(x => x.KindergartenId == request.KindergartenId);
            
        if (request.Email is not null)
            query = query.Where(x => x.User.Email.Contains(request.Email));
        
        if (request.FirstName is not null)
            query = query.Where(x => x.User.FirstName.Contains(request.FirstName));
        
        if (request.LastName is not null)
            query = query.Where(x => x.User.LastName.Contains(request.LastName));
        
        var employeeDtos = await query.
            Select(x => new EmployeeDto(x.Id, x.User.FirstName + " " + x.User.LastName,
                x.EmployeePosition.Name, x.User.Email))
            .ToListAsync(cancellationToken);
        
        return new GetAllEmployeesForSingleKindergartenQueryResponseDto(employeeDtos);
    }
}

