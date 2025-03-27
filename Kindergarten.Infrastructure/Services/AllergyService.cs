using Kindergarten.Application.Common.Dto.Children;
using Kindergarten.Application.Common.Dto.Parent;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class AllergyService(IKindergartenDbContext kindergartenDbContext) : IAllergyService
{
    public async Task<Guid> CreateAllergy(string allergy, CancellationToken cancellationToken)
    {
        var newAllergy = new Allergy
        {
            Name = allergy
        };
        kindergartenDbContext.Allergies.Add(newAllergy);
        await kindergartenDbContext.SaveChangesAsync(cancellationToken);
        return newAllergy.Id;
    }

    public async Task CreateAllergiesForChildrenThroughParentRequest(List<NewChildrenThroughParentRequestDto> children,
        CancellationToken cancellationToken)
    {
        foreach (var child in children)
        {
            if (child.HasAllergies)
            {
                foreach (var allergy in child.Allergies!)
                {
                    var normalizedAllergyName = allergy.Trim();

                    var existingAllergy = await kindergartenDbContext.Allergies
                        .FirstOrDefaultAsync(a => a.Name.ToLower() == normalizedAllergyName.ToLower(),
                            cancellationToken);

                    var childAllergies = new ChildAllergy();

                    if (existingAllergy == null)
                    {
                        var newAllergyId = await CreateAllergy(normalizedAllergyName, cancellationToken);
                        childAllergies.AllergyId = newAllergyId;
                    }
                    else
                    {
                        childAllergies.AllergyId = existingAllergy.Id;
                    }

                    childAllergies.ChildId = child.Id;

                    kindergartenDbContext.ChildAllergies.Add(childAllergies);
                    await kindergartenDbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }


    }

    public async Task CreateAllergiesForNewChild(Guid childId, bool hasAllergies, List<string>? allergies,
        CancellationToken cancellationToken)
    {
        if (hasAllergies)
        {
            foreach (var allergy in allergies!)
            {
                var normalizedAllergyName = allergy.Trim();

                var existingAllergy = await kindergartenDbContext.Allergies
                    .FirstOrDefaultAsync(a => a.Name.ToLower() == normalizedAllergyName.ToLower(), cancellationToken);

                var childAllergies = new ChildAllergy();

                if (existingAllergy == null)
                {
                    var newAllergyId = await CreateAllergy(normalizedAllergyName, cancellationToken);
                    childAllergies.AllergyId = newAllergyId;
                }
                else
                {
                    childAllergies.AllergyId = existingAllergy.Id;
                }

                childAllergies.ChildId = childId;

                kindergartenDbContext.ChildAllergies.Add(childAllergies);
            }
        }
    }
}