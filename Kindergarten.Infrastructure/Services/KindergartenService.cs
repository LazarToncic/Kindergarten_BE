using Kindergarten.Application.Common.Exceptions;
using Kindergarten.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Services;

public class KindergartenService(IKindergartenDbContext dbContext) : IKindergartenService
{
    public async Task<Guid> GetKindergartenId(string kindergartenName)
    {
        var kindergarten = await dbContext.Kindergartens
            .Where(x => x.Name.Equals(kindergartenName))
            .FirstOrDefaultAsync();

        if (kindergarten == null)
            throw new NotFoundException("Kindergarten with this name doesnt exist",
                new {kindergartenName});

        return kindergarten.Id;
    }

    public async Task<string> GetKindergartenName(Guid kindergartenId)
    {
        var kindergarten = await dbContext.Kindergartens
            .Where(x => x.Id.Equals(kindergartenId))
            .FirstOrDefaultAsync();

        if (kindergarten == null)
            throw new NotFoundException("Kindergarten with this Name doesnt exist");

        return kindergarten.Name;
    }
}