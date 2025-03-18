using System.Text.Json;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;

namespace Kindergarten.Infrastructure.Services;

public class ChildrenService(IKindergartenDbContext dbContext) : IChildrenService
{
    public async Task AddChildrenThroughParentRequest(string jsonChildren, Guid parentId,
        CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var childrenDtos = JsonSerializer.Deserialize<List<ParentRequestChildDto>>(jsonChildren, options);
        
        foreach (var childDto in childrenDtos)
        {
            var child = new Child
            {
                FirstName = childDto.FirstName,
                LastName = childDto.LastName,
                YearOfBirth = childDto.DateOfBirth
            };
        
            // Dodaj entitet u DbContext
            dbContext.Children.Add(child);
        
            // Ako imaš dodatne servise za alergije i medicinske uslove,
            // ovde možeš pozvati i njih, ili nakon petlje
            // await allergyService.AddAllergiesForChildAsync(child.Id, childDto.Allergies, cancellationToken);
            // await medicalService.AddMedicalConditionsForChildAsync(child.Id, childDto.MedicalConditions, cancellationToken);
        }
    
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}