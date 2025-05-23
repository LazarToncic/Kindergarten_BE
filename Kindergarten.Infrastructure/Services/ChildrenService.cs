using System.Text.Json;
using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Extensions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Children;
using Kindergarten.Application.Common.Repositories;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ChildrenService(IKindergartenDbContext dbContext, IAllergyService allergyService, 
    IMedicalConditionService medicalConditionService, ICurrentUserService currentUserService, 
    ICoordinatorService coordinatorService, IKindergartenService kindergartenService, IDepartmentEmployeeRepository departmentEmployeeRepository) : IChildrenService
{
    public async Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId,
        ParentChildRelationship relationship, string preferredKindergarten, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var childrenDtos = JsonSerializer.Deserialize<List<ParentRequestChildDto>>(jsonChildren, options);
        
        var createdChildren = new List<NewChildrenThroughParentRequestDto>();
        
        var preferredKindergartenId = await kindergartenService.GetKindergartenId(preferredKindergarten);
        
        foreach (var childDto in childrenDtos)
        {
            var child = new Child
            {
                FirstName = childDto.FirstName,
                LastName = childDto.LastName,
                YearOfBirth = childDto.DateOfBirth,
                RequestedKindergartenId = preferredKindergartenId
            };
            
            dbContext.Children.Add(child);

            var parentChildren = new ParentChild
            {
                ChildId = child.Id,
                ParentId = parentId,
                Relationship = relationship
            };
            
            dbContext.ParentChildren.Add(parentChildren);
            
            createdChildren.Add(ChildrenMapper.
                FromParentRequestChildDtoToNewChildrenThroughParentRequest(child.Id, childDto));
            
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);    
        
        await allergyService.CreateAllergiesForChildrenThroughParentRequest(createdChildren, cancellationToken);
        
        await medicalConditionService.CreateMedicalConditiionsForChildrenThroughParentRequest(createdChildren, cancellationToken);

    }

    public async Task AddNewChild(string firstName, string lastName, DateOnly dateOfBirth, bool hasAllergies, List<string>? allergies,
        bool hasMedicalIssues, List<string>? medicalConditions, ParentChildRelationship parentChildRelationship, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var child = new Child
            {
                FirstName = firstName,
                LastName = lastName,
                YearOfBirth = dateOfBirth
            };
            
            dbContext.Children.Add(child);
            
            var parentId = await GetParentIdWithUserId(userId!, cancellationToken);

            var childParent = new ParentChild
            {
                ParentId = parentId,
                ChildId = child.Id,
                Relationship = parentChildRelationship
            };
            
            dbContext.ParentChildren.Add(childParent);

            await allergyService.CreateAllergiesForNewChild(child.Id, hasAllergies, allergies, cancellationToken);
            await medicalConditionService.CreateMedicalConditionsForNewChild(child.Id, hasMedicalIssues, medicalConditions, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<GetUnassignedChildrenListDto> GetUnassignedChildren(Guid? kindergartenId, string? firstName,
        string? lastName, CancellationToken cancellationToken)
    {
        var roles = currentUserService.Roles!;

        IQueryable<Child> query = dbContext.Children
            .Where(c => !c.DepartmentAssignments.Any());

        if (roles.Contains(RolesExtensions.Owner) || roles.Contains(RolesExtensions.Manager))
        {
            if (kindergartenId.HasValue)
            {
                var kgId = kindergartenId.Value;
                query = query.Where(c =>
                    !c.DepartmentAssignments
                        .Any(cd =>
                            cd.IsActive
                            && cd.Department
                                .KindergartenDepartments
                                .Any(kd => kd.KindergartenId == kgId)
                        ));
            }
        }
        else if (roles.Contains(RolesExtensions.Coordinator))
        {
            var myKgId = await coordinatorService
                .GetKindergartenIdForCoordinator(currentUserService.UserId!, cancellationToken);

            query = query.Where(c =>
                c.RequestedKindergartenId == myKgId &&
                !c.DepartmentAssignments.Any());
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to view unassigned children.");
        }

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(c => c.FirstName.Contains(firstName!));

        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(c => c.LastName.Contains(lastName!));

        var children = await query
            .Include(c => c.ChildAllergies)
            .ThenInclude(ca => ca.Allergy)
            .Include(c => c.ChildMedicalConditions)
            .ThenInclude(cm => cm.MedicalCondition)
            .ToListAsync(cancellationToken);

        var list = children
            .Select(ChildrenMapper.ChildToGetUnassignedChildrenDto)
            .ToList();

        return new GetUnassignedChildrenListDto(list);
    }

    public async Task<int> GetChildrenAge(Guid childrenId, CancellationToken cancellationToken)
    {
        var child = await dbContext.Children
            .FirstOrDefaultAsync(c => c.Id == childrenId, cancellationToken);

        if (child is null)
            throw new NotFoundException("Child not found.");

        int birthYear = child.YearOfBirth.Year;
        int currentYear = DateTime.Today.Year;

        return currentYear - birthYear;
    }

    public async Task AssignChildToDepartment(Guid childId, Guid departmentId, CancellationToken cancellationToken)
    {
        var kindergartenId = await kindergartenService.GetKindergartenIdWithDepartmentId(departmentId);

        var childDepartmentAssigned = new ChildDepartment
        {
            ChildId = childId,
            DepartmentId = departmentId,
            KindergartenId = kindergartenId,
            IsActive = true,
            AssignedByUserId = currentUserService.UserId!,
        };
        
        dbContext.ChildDepartments.Add(childDepartmentAssigned);
        
        var department = await dbContext.Departments
            .FirstOrDefaultAsync(x => x.Id == departmentId, cancellationToken);
        
        if (department is null)
            throw new NotFoundException("Department not found.");

        department.Capacity += 1;
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetChildrenQueryResponseList> GetChildren(Guid? kindergartenId, string? firstName, string? lastName, DateOnly? dateOfBirth,
         bool? isActive, CancellationToken cancellationToken)
    {
        var roles = currentUserService.Roles!;
        
        var isOwnerOrManager = roles.Contains(RolesExtensions.Owner)
                               || roles.Contains(RolesExtensions.Manager);
        var isCoordinator    = !isOwnerOrManager
                               && roles.Contains(RolesExtensions.Coordinator);
        var isEmployeeOnly   = !isOwnerOrManager
                               && !isCoordinator
                               && roles.Contains(RolesExtensions.Employee);
        
        IQueryable<Child> baseQuery = dbContext.Children;

        // 1) Role-based + kindergartenId + isActive filters
        if (isOwnerOrManager)
        {
            if (kindergartenId.HasValue)
            {
                baseQuery = baseQuery.Where(c =>
                    c.DepartmentAssignments.Any(da =>
                        da.KindergartenId == kindergartenId.Value &&
                        (!isActive.HasValue || da.IsActive == isActive.Value)
                    ));
            }
        }
        else if (isCoordinator)
        {
            var myKgId = await coordinatorService
                .GetKindergartenIdForCoordinator(currentUserService.UserId!, cancellationToken);

            baseQuery = baseQuery.Where(c =>
                c.DepartmentAssignments.Any(da =>
                    da.KindergartenId == myKgId &&
                    (!isActive.HasValue || da.IsActive == isActive.Value)
                ));
        }
        else if (isEmployeeOnly)
        {
            // leave baseQuery untouched for now
        }
        else
        {
            throw new UnauthorizedAccessException("You are not authorized to view children.");
        }

        // 2) Additional filters
        if (!string.IsNullOrWhiteSpace(firstName))
            baseQuery = baseQuery.Where(c => c.FirstName.Contains(firstName));
        if (!string.IsNullOrWhiteSpace(lastName))
            baseQuery = baseQuery.Where(c => c.LastName.Contains(lastName));
        if (dateOfBirth.HasValue)
            baseQuery = baseQuery.Where(c => c.YearOfBirth == dateOfBirth.Value);

        // 3) Eager loading
        var childrenQuery   = baseQuery
            .Include(c => c.DepartmentAssignments)
                .ThenInclude(da => da.Department)
                    .ThenInclude(d => d.KindergartenDepartments)
                        .ThenInclude(kd => kd.Kindergarten)
            .Include(c => c.DepartmentAssignments)
                .ThenInclude(da => da.Department)
                    .ThenInclude(d => d.DepartmentEmployees)
                        .ThenInclude(de => de.Employee)
                            .ThenInclude(e => e.User)
            .Include(c => c.ParentChildren)
                .ThenInclude(pc => pc.Parent)
                    .ThenInclude(p => p.User);

        // 2) Materializuj query u listu
        var childrenList = await childrenQuery
            .ToListAsync(cancellationToken);

    // 3) Ako je teacher-only, filtriraj tu novu listu
        if (isEmployeeOnly)
        {
            var currentUserId = currentUserService.UserId!;
            var filtered = new List<Child>();

            foreach (var kid in childrenList)
            {
                var assignment = kid.DepartmentAssignments
                    .FirstOrDefault(da => !isActive.HasValue || da.IsActive == isActive.Value);
                if (assignment == null) 
                    continue;

                var isTeacher = await departmentEmployeeRepository
                    .IsTeacherInDepartmentAsync(
                        assignment.DepartmentId,
                        currentUserId,
                        assignment.KindergartenId,
                        cancellationToken);

                if (isTeacher)
                    filtered.Add(kid);
            }

            childrenList = filtered;
        }

        return ChildrenMapper.ToGetChildrenQueryResponseList(childrenList);
        
    }

    private async Task<Guid> GetParentIdWithUserId(string userId, CancellationToken cancellationToken)
    {
        if (userId == null)
            throw new NotFoundException("User id cannot be null.");
        
        var parentId = await dbContext.Parents
            .Where(x => x.UserId == userId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (parentId == Guid.Empty)
            throw new NotFoundException("Parent id cannot be null.");
        
        return parentId;
    }
}