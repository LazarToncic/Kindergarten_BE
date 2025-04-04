using System.Text.Json;
using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Children;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class ChildrenService(IKindergartenDbContext dbContext, IAllergyService allergyService, 
    IMedicalConditionService medicalConditionService, ICurrentUserService currentUserService) : IChildrenService
{
    public async Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId,
        ParentChildRelationship relationship, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var childrenDtos = JsonSerializer.Deserialize<List<ParentRequestChildDto>>(jsonChildren, options);
        
        var createdChildren = new List<NewChildrenThroughParentRequestDto>();
        
        foreach (var childDto in childrenDtos)
        {
            var child = new Child
            {
                FirstName = childDto.FirstName,
                LastName = childDto.LastName,
                YearOfBirth = childDto.DateOfBirth
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

    public async Task AddNewChild(string firstName, string lastName, DateTime dateOfBirth, bool hasAllergies, List<string>? allergies,
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
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
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