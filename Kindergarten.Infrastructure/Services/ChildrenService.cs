using System.Text.Json;
using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Application.Common.Mappers.Children;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Entities.Enums;

namespace Kindergarten.Infrastructure.Services;

public class ChildrenService(IKindergartenDbContext dbContext, IAllergyService allergyService, 
    IMedicalConditionService medicalConditionService) : IChildrenService
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
}