using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class MedicalConditionService(IKindergartenDbContext dbContext) : IMedicalConditionService
{
    public async Task CreateMedicalConditiionsForChildrenThroughParentRequest(
        List<NewChildrenThroughParentRequestDto> children,
        CancellationToken cancellationToken)
    {
        foreach (var child in children)
        {
            if (child.HasMedicalIssues)
            {
                foreach (var medicalCondition in child.MedicalConditions!)
                {
                    var normalizedMedicalConditionName = medicalCondition.Trim();

                    var existingMedicalCondition = await dbContext.MedicalConditions
                        .FirstOrDefaultAsync(a => a.Name.ToLower() == normalizedMedicalConditionName.ToLower(),
                            cancellationToken);

                    var childMedicalCondition = new ChildMedicalCondition();

                    if (existingMedicalCondition == null)
                    {
                        var newMedicalConditionId = await CreateNewMedicalCondition(normalizedMedicalConditionName, cancellationToken);
                        childMedicalCondition.MedicalConditionId = newMedicalConditionId;
                    }
                    else
                    {
                        childMedicalCondition.MedicalConditionId = existingMedicalCondition.Id;
                    }

                    childMedicalCondition.ChildId = child.Id;

                    dbContext.ChildMedicalConditions.Add(childMedicalCondition);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }

    public async Task CreateMedicalConditionsForNewChild(Guid childId, bool hasMedicalIssues, List<string>? medicalConditions,
        CancellationToken cancellationToken)
    {
        if (hasMedicalIssues)
        {
            foreach (var medicalCondition in medicalConditions!)
            {
                var normalizedMedicalConditionName = medicalCondition.Trim();

                var existingMedicalCondition = await dbContext.MedicalConditions
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == normalizedMedicalConditionName.ToLower(),
                        cancellationToken);

                var childMedicalCondition = new ChildMedicalCondition();

                if (existingMedicalCondition == null)
                {
                    var newMedicalConditionId =
                        await CreateNewMedicalCondition(normalizedMedicalConditionName,
                            cancellationToken);
                    childMedicalCondition.MedicalConditionId = newMedicalConditionId;
                }
                else
                {
                    childMedicalCondition.MedicalConditionId = existingMedicalCondition.Id;
                }

                childMedicalCondition.ChildId = childId;

                dbContext.ChildMedicalConditions.Add(childMedicalCondition);
            }
        }
    }

    private async Task<Guid> CreateNewMedicalCondition(string medicalConditionName,
        CancellationToken cancellationToken)
    {
        var newMedicalCondition = new MedicalCondition 
        { 
            Name = medicalConditionName 
        };
        dbContext.MedicalConditions.Add(newMedicalCondition);
        await dbContext.SaveChangesAsync(cancellationToken);
        return newMedicalCondition.Id;
    }

}